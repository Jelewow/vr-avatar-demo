using System;
using UnityEngine;

namespace Jelewow.XR
{
    public interface IXRPlayer
    {
        Transform Head { get; }
        Transform LeftHand { get; }
        Transform RightHand { get; }
        event Action Teleported;
    }
}