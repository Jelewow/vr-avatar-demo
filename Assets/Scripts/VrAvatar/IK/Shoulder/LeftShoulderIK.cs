using UnityEngine;

namespace Jelewow.VrAvatar.IK
{
    public class LeftShoulderIK : ShoulderIK
    {
        protected override Transform Hand => BodyIK.Player.XRPlayer.LeftHand;
        protected override HumanBodyBones Shoulder => HumanBodyBones.LeftShoulder;
    }
}