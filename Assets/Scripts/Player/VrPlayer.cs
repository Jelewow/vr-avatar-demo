using Jelewow.XR;
using UnityEngine;

namespace Jelewow.Player
{
    public class VrPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _avatarVisual;
        
        public IXRPlayer XRPlayer { get; private set; }

        private void Awake()
        {
            XRPlayer = GetComponent<IXRPlayer>();
            _avatarVisual.SetActive(true);
        }
    }
}