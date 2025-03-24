using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jelewow.VrAvatar.IK
{
    public class LeftHandIK : HandIK
    {
        protected override AvatarIKGoal Hand => AvatarIKGoal.LeftHand;

        protected override Transform GetGoalTransform()
        {
            if (!ControllerHand)
            {
                return BodyIK.Player.XRPlayer.LeftHand;
            }
            
            var bones = ControllerHand.bones.ToList();
            FillFingerDictionary(bones);
            return ControllerHand.rootBone;
        }

        protected override void FillFingerDictionary(List<Transform> bones)
        {
            FingerBones.Add(HumanBodyBones.LeftThumbProximal, bones.Find(bone => bone.name == "LeftHandThumb1"));
            FingerBones.Add(HumanBodyBones.LeftThumbIntermediate, bones.Find(bone => bone.name == "LeftHandThumb2"));
            FingerBones.Add(HumanBodyBones.LeftThumbDistal, bones.Find(bone => bone.name == "LeftHandThumb3"));

            FingerBones.Add(HumanBodyBones.LeftIndexProximal, bones.Find(bone => bone.name == "LeftHandIndex1"));
            FingerBones.Add(HumanBodyBones.LeftIndexIntermediate, bones.Find(bone => bone.name == "LeftHandIndex2"));
            FingerBones.Add(HumanBodyBones.LeftIndexDistal, bones.Find(bone => bone.name == "LeftHandIndex3"));

            FingerBones.Add(HumanBodyBones.LeftMiddleProximal, bones.Find(bone => bone.name == "LeftHandMiddle1"));
            FingerBones.Add(HumanBodyBones.LeftMiddleIntermediate, bones.Find(bone => bone.name == "LeftHandMiddle2"));
            FingerBones.Add(HumanBodyBones.LeftMiddleDistal, bones.Find(bone => bone.name == "LeftHandMiddle3"));

            FingerBones.Add(HumanBodyBones.LeftRingProximal, bones.Find(bone => bone.name == "LeftHandRing1"));
            FingerBones.Add(HumanBodyBones.LeftRingIntermediate, bones.Find(bone => bone.name == "LeftHandRing2"));
            FingerBones.Add(HumanBodyBones.LeftRingDistal, bones.Find(bone => bone.name == "LeftHandRing3"));

            FingerBones.Add(HumanBodyBones.LeftLittleProximal, bones.Find(bone => bone.name == "LeftHandPinky1"));
            FingerBones.Add(HumanBodyBones.LeftLittleIntermediate, bones.Find(bone => bone.name == "LeftHandPinky2"));
            FingerBones.Add(HumanBodyBones.LeftLittleDistal, bones.Find(bone => bone.name == "LeftHandPinky3"));
        }
    }
}