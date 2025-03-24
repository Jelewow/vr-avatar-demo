using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jelewow.VrAvatar.IK
{
    public class RightHandIK : HandIK
    {
        protected override AvatarIKGoal Hand => AvatarIKGoal.RightHand;
        
        protected override Transform GetGoalTransform()
        {
            if (!ControllerHand)
            {
                return BodyIK.Player.XRPlayer.RightHand;
            }
            
            var bones = ControllerHand.bones.ToList();
            FillFingerDictionary(bones);
            return ControllerHand.rootBone;
        }
        
        protected override void FillFingerDictionary(List<Transform> bones)
        {
            FingerBones.Add(HumanBodyBones.RightThumbProximal, bones.Find(bone => bone.name == "LeftHandThumb1"));
            FingerBones.Add(HumanBodyBones.RightThumbIntermediate, bones.Find(bone => bone.name == "LeftHandThumb2"));
            FingerBones.Add(HumanBodyBones.RightThumbDistal, bones.Find(bone => bone.name == "LeftHandThumb3"));

            FingerBones.Add(HumanBodyBones.RightIndexProximal, bones.Find(bone => bone.name == "LeftHandIndex1"));
            FingerBones.Add(HumanBodyBones.RightIndexIntermediate, bones.Find(bone => bone.name == "LeftHandIndex2"));
            FingerBones.Add(HumanBodyBones.RightIndexDistal, bones.Find(bone => bone.name == "LeftHandIndex3"));

            FingerBones.Add(HumanBodyBones.RightMiddleProximal, bones.Find(bone => bone.name == "LeftHandMiddle1"));
            FingerBones.Add(HumanBodyBones.RightMiddleIntermediate, bones.Find(bone => bone.name == "LeftHandMiddle2"));
            FingerBones.Add(HumanBodyBones.RightMiddleDistal, bones.Find(bone => bone.name == "LeftHandMiddle3"));

            FingerBones.Add(HumanBodyBones.RightRingProximal, bones.Find(bone => bone.name == "LeftHandRing1"));
            FingerBones.Add(HumanBodyBones.RightRingIntermediate, bones.Find(bone => bone.name == "LeftHandRing2"));
            FingerBones.Add(HumanBodyBones.RightRingDistal, bones.Find(bone => bone.name == "LeftHandRing3"));

            FingerBones.Add(HumanBodyBones.RightLittleProximal, bones.Find(bone => bone.name == "LeftHandPinky1"));
            FingerBones.Add(HumanBodyBones.RightLittleIntermediate, bones.Find(bone => bone.name == "LeftHandPinky2"));
            FingerBones.Add(HumanBodyBones.RightLittleDistal, bones.Find(bone => bone.name == "LeftHandPinky3"));
        }
    }
}