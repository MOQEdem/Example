using UnityEngine;
using UnityEngine.UI;

namespace _ProjectAssets.Scripts.Pointers
{
    public class PointerView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            SetIconRotation(Quaternion.Euler(Vector3.zero));
        }

        public void SetIcon(Sprite icon) => 
            _icon.sprite = icon;

        private void SetIconRotation(Quaternion rotation) => 
            _icon.transform.rotation = rotation;

        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);
    }
}