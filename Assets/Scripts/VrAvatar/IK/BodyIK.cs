using Jelewow.Player;
using Jelewow.VrAvatar.Animations;
using UnityEngine;

namespace Jelewow.VrAvatar.IK
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AvatarBot))]
    public class BodyIK : MonoBehaviour
    {
        [Header("General")]
        [field : SerializeField] public VrPlayer Player { get; private set; }
        [SerializeField] private AvatarBot _bot;
        
        [Header("Settings")]
        [SerializeField] private float _offsetBetweenHeadAndHeadset;
        [SerializeField] private float _footRotationOffsetLowerBorder;
        [SerializeField] private float _footRotationOffsetUpperBorder;
        [SerializeField] private float _deltaVelocityThreshold = 0.01f;
        [SerializeField] private float _footOffset = 0.1f;
        [SerializeField] private float _pow;
        [SerializeField] private float _botHeight;

        private Animator _animator;
        private Transform _playerHead;
        
        private Vector3 _previousPosition;
        private Vector3 _previousVelocity;

        private float _avatarScale = 1f;
        private float _path;

        public float Scale => _avatarScale;
        
        private Vector3 HeadPosition => _playerHead.position + _playerHead.rotation * Vector3.back * _offsetBetweenHeadAndHeadset;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerHead = Player.XRPlayer.Head;
        }
        
        private void OnEnable()
        {
            Player.XRPlayer.Teleported += OnPlayerTeleported;
        }

        private void OnDisable()
        {
            Player.XRPlayer.Teleported -= OnPlayerTeleported;
        }
        
        private void Update()
        {
            if (!_playerHead)
            {
                return;
            }

            var delta = HeadPosition - _previousPosition;
            var projectedDelta = Vector3.ProjectOnPlane(delta, Vector3.up);
            var xVelocity = Vector3.Dot(_playerHead.right, projectedDelta);
            var zVelocity = Vector3.Dot(_playerHead.forward, projectedDelta);

            var velocity = new Vector3(xVelocity, 0, zVelocity) / Time.deltaTime;

            var newVelocity = Vector3.Lerp(_previousVelocity, velocity, Time.deltaTime * 10f);
            _path += new Vector3(delta.x, 0f, delta.z).magnitude;
            var time = _path - Mathf.Floor(_path);
            var endVelocityX = Mathf.Pow(Mathf.Abs(newVelocity.x), _pow) * Mathf.Sign(newVelocity.x);
            var endVelocityY = Mathf.Pow(Mathf.Abs(newVelocity.z), _pow) * Mathf.Sign(newVelocity.z);

            var isMoving = newVelocity.magnitude > _deltaVelocityThreshold;

            if (!isMoving)
            {
                _path = Mathf.SmoothStep(_path, Mathf.Floor(_path), Time.deltaTime);
            }

            _animator.SetBool(AvatarAnimations.IsMoving, isMoving);

            _animator.SetFloat(AvatarAnimations.VelocityX, endVelocityX);
            _animator.SetFloat(AvatarAnimations.VelocityY, endVelocityY);
            _animator.SetFloat(AvatarAnimations.Time, time);

            _previousVelocity = newVelocity;
            _previousPosition = HeadPosition;
        }

        private void LateUpdate()
        {
            if (!_playerHead)
            {
                return;
            }
            
            UpdateRigPosition();
        }
        
        private void OnAnimatorIK(int layerIndex)
        {
            if (!_playerHead)
            {
                return;
            }

            var projectedForward = Quaternion.Euler(0f, -90f, 0f) * Vector3.ProjectOnPlane(_playerHead.right, Vector3.up).normalized;
            var bodyRotation = Quaternion.LookRotation(projectedForward, Vector3.up);
            transform.rotation = bodyRotation;

            var height = Mathf.Clamp(GetCurrentPlayerHeight() - 1, 0, 2);
            var power = Mathf.Clamp01(Mathf.InverseLerp(_botHeight - 1, 0, height));
            var powerForHips = power * 60;
            
            var hipsRotation = Quaternion.AngleAxis(powerForHips, Vector3.right);
            var headRotation = Quaternion.Inverse(bodyRotation) * _playerHead.rotation * Quaternion.Inverse(hipsRotation);
            var leftUpperLegRotation = _bot.LeftUpperLeg.localRotation * Quaternion.AngleAxis(powerForHips / 6f, Vector3.up);
            var rightUpperLegRotation = _bot.RightUpperLeg.localRotation * Quaternion.AngleAxis(-powerForHips / 6f, Vector3.up);

            _animator.SetBoneLocalRotation(HumanBodyBones.Hips, hipsRotation);
            _animator.SetBoneLocalRotation(HumanBodyBones.Head, headRotation);
            _animator.SetBoneLocalRotation(HumanBodyBones.LeftUpperLeg, leftUpperLegRotation);
            _animator.SetBoneLocalRotation(HumanBodyBones.RightUpperLeg, rightUpperLegRotation);

            SetFoot(AvatarIKGoal.LeftFoot, powerForHips / 6f);
            SetFoot(AvatarIKGoal.RightFoot, powerForHips / 6f, false);
        }
        
        public void ScaleAvatar()
        {
            var playerHeight = GetCurrentPlayerHeight();

            _avatarScale = Mathf.Clamp(playerHeight, 1.2f, 2.2f) / _botHeight;
            transform.localScale = Vector3.one * _avatarScale;
        }

        public void ScaleAvatar(float scale)
        {
            _avatarScale = scale;
            transform.localScale = Vector3.one * scale;
        }
        
        private void UpdateRigPosition()
        {
            var projectedForward = Quaternion.Euler(0f, -90f, 0f) * Vector3.ProjectOnPlane(_playerHead.right, Vector3.up).normalized;
            var bodyRotation = Quaternion.LookRotation(projectedForward, Vector3.up);

            transform.rotation = bodyRotation;
            transform.position = HeadPosition + Vector3.down * (_bot.Head.position.y - _bot.Hips.position.y);
            transform.position += Vector3.ProjectOnPlane(_bot.Hips.position - _bot.Head.position, Vector3.up);
        }
        
        private void OnPlayerTeleported()
        {
            _path = 0f;
            _previousPosition = _playerHead.position;
            _previousVelocity = Vector3.zero;
        }

        private void SetFoot(AvatarIKGoal goal, float heightFactor, bool isLeft = true)
        {
            var footPosition = _animator.GetIKPosition(goal);
            var ray = new Ray(footPosition + Vector3.up, Vector3.down);

            var hits = new RaycastHit[10];

            if (Physics.RaycastNonAlloc(ray, hits, 3f) == 0)
            {
                _animator.SetIKPositionWeight(goal, 0);
                return;
            }

            RaycastHit? validHit = null;

            foreach (var hit in hits)
            {
                if (!hit.collider)
                {
                    continue;
                }

                validHit = hit;
                break;
            }

            if (validHit == null)
            {
                _animator.SetIKPositionWeight(goal, 0);
            }
            else
            {
                var hit = (RaycastHit)validHit;

                var turningAngle = (isLeft ? -1f : 1f) * Mathf.Clamp(heightFactor * _footRotationOffsetUpperBorder, _footRotationOffsetLowerBorder, _footRotationOffsetUpperBorder);
                var turningOffsetRotation = Quaternion.AngleAxis(turningAngle, Vector3.up);
                var boneTransformProjection = Vector3.ProjectOnPlane(transform.forward, hit.normal);
                var footRotation = Quaternion.LookRotation(boneTransformProjection, hit.normal) * turningOffsetRotation;

                _animator.SetIKRotationWeight(goal, 1);
                _animator.SetIKRotation(goal, footRotation);

                _animator.SetIKPositionWeight(goal, validHit.Value.distance > 2f ? 0 : 1);
                _animator.SetIKPosition(goal, hit.point + Vector3.up * _footOffset);
            }
        }
        
        private float GetCurrentPlayerHeight()
        {
            const float EyesOffset = 0.1f;
            var playerHeight = _playerHead.localPosition.y + EyesOffset;
            return playerHeight;
        }
    }
}