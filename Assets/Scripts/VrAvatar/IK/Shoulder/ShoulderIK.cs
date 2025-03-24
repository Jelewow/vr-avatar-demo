using UnityEngine;

namespace Jelewow.VrAvatar.IK
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BodyIK))]
    public abstract class ShoulderIK : MonoBehaviour
    {
        protected BodyIK BodyIK;

        [SerializeField] private float _rotationMultiply;
        [SerializeField] private float _minRotation;
        [SerializeField] private float _maxRotation;

        private Animator _animator;
        private Quaternion _initialShoulderRotation;

        protected abstract Transform Hand { get; }
        protected abstract HumanBodyBones Shoulder { get; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            BodyIK = GetComponent<BodyIK>();
        }

        private void Start()
        {
            _initialShoulderRotation = _animator.GetBoneTransform(Shoulder).localRotation;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            var handHeightDelta = _animator.GetBoneTransform(Shoulder).position.y - Hand.position.y;
            var clampedRotationX = Mathf.Clamp(handHeightDelta * _rotationMultiply, _minRotation, _maxRotation);
            var targetRotation = _initialShoulderRotation * Quaternion.Euler(clampedRotationX, 0, 0);
            _animator.SetBoneLocalRotation(Shoulder, targetRotation);
        }
    }
}