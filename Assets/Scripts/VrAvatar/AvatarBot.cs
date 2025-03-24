using UnityEngine;

namespace Jelewow.VrAvatar
{
    [RequireComponent(typeof(Animator))]
    public class AvatarBot : MonoBehaviour
    {
        private Animator _animator;
        
        public Transform Hips { get; private set; }
        
        public Transform Head { get; private set; }
        
        public Transform LeftHand { get; private set; }
        
        public Transform RightHand { get; private set; }
        
        public Transform LeftUpperLeg { get; private set; }
        
        public Transform RightUpperLeg { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            Hips = _animator.GetBoneTransform(HumanBodyBones.Hips);
            Head = _animator.GetBoneTransform(HumanBodyBones.Head);
            LeftHand = _animator.GetBoneTransform(HumanBodyBones.LeftHand);
            RightHand = _animator.GetBoneTransform(HumanBodyBones.RightHand);
            LeftUpperLeg = _animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
            RightUpperLeg = _animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
        }
    }
}