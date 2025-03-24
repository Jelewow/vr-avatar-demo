using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Jelewow.VrAvatar.IK
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BodyIK))]
    public abstract class HandIK : MonoBehaviour
    {
        protected readonly Dictionary<HumanBodyBones, Transform> FingerBones = new();

        protected BodyIK BodyIK;
        
        [SerializeField] protected SkinnedMeshRenderer ControllerHand;
        [SerializeField] private Vector3 _fingerOffset;
        [SerializeField] private Vector3 _handOffset;
        
        private Animator _animator;
        private Transform _target;
        
        protected abstract AvatarIKGoal Hand { get; }
        protected abstract Transform GetGoalTransform();
        protected abstract void FillFingerDictionary(List<Transform> bones);

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            BodyIK = GetComponent<BodyIK>();
            _target = GetGoalTransform();
        }
        
        private void OnAnimatorIK(int layerIndex)
        {
            if (!_target)
            {
                _animator.SetIKPositionWeight(Hand, 0);
                _animator.SetIKRotationWeight(Hand, 0);
                return;
            }
            
            _animator.SetIKPosition(Hand, _target.position);
            _animator.SetIKRotation(Hand, _target.rotation * Quaternion.Euler(_handOffset));
            _animator.SetIKPositionWeight(Hand, 1);
            _animator.SetIKRotationWeight(Hand, 1);
            
            foreach (var pose in FingerBones)
            {
                var newLocalRotation = Quaternion.Inverse(pose.Value.parent.rotation) * pose.Value.rotation;
                _animator.SetBoneLocalRotation(pose.Key, newLocalRotation * Quaternion.Euler(_fingerOffset));
            }
        }
    }
}