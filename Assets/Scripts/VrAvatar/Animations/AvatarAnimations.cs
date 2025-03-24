using UnityEngine;

namespace Jelewow.VrAvatar.Animations
{
    public static class AvatarAnimations
    {
        public static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));
        public static readonly int VelocityX = Animator.StringToHash(nameof(VelocityX));
        public static readonly int VelocityY = Animator.StringToHash(nameof(VelocityY));
        public static readonly int Time = Animator.StringToHash(nameof(Time));
    }
}