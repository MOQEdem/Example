using UnityEngine;

namespace _ProjectAssets.Scripts.Pointers
{
    public class Pointer : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;

        public Sprite Icon => _icon;

        private void OnEnable() => 
            On();

        private void OnDisable() => 
            Off();

        [ContextMenu("On")]
        public void On() => 
            PointerManager.Instance?.Add(this);

        [ContextMenu("Off")]
        public void Off() => 
            PointerManager.Instance?.Remove(this);
    }
}