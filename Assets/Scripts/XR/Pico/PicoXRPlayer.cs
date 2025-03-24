using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace Jelewow.XR.Pico
{
    public class PicoXRPlayer : MonoBehaviour, IXRPlayer
    {
        [SerializeField] private TeleportationProvider _teleportationProvider;
        
        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public Transform LeftHand { get; private set; }
        [field: SerializeField] public Transform RightHand { get; private set; }
        
        public event Action Teleported;

        private void OnEnable()
        {
            SetupTeleportEvents();
        }
        
        private void OnDisable()
        {
            TeardownTeleportEvents();
        }
        
        private void SetupTeleportEvents()
        {
            _teleportationProvider.locomotionEnded += OnTeleportEnd;
        }

        private void TeardownTeleportEvents()
        {
            _teleportationProvider.locomotionEnded -= OnTeleportEnd;
        }
        
        private void OnTeleportEnd(LocomotionProvider locomotionProvider)
        {
            Teleported?.Invoke();
        }
    }
}