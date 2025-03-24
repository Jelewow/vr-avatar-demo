using UnityEngine;

namespace Jelewow.VrAvatar.IK
{
    public class RightShoulderIK : ShoulderIK
    {
        protected override Transform Hand => BodyIK.Player.XRPlayer.RightHand;
        protected override HumanBodyBones Shoulder => HumanBodyBones.RightShoulder;
    }
}