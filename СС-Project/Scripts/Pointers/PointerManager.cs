using System.Collections.Generic;
using UnityEngine;

namespace _ProjectAssets.Scripts.Pointers
{
    public class PointerManager : MonoBehaviour
    {
        [SerializeField] private PointerView _pointerViewPrefab;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private bool _isAlwaysShown;

        private readonly Dictionary<Pointer, PointerView> _pointersMap = new Dictionary<Pointer, PointerView>();

        public static PointerManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void LateUpdate()
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            foreach (KeyValuePair<Pointer, PointerView> kvp in _pointersMap)
            {
                Pointer pointer = kvp.Key;
                PointerView pointerView = kvp.Value;

                Vector3 toPointer = pointer.transform.position - _playerTransform.position;
                Ray ray = new Ray(_playerTransform.position, toPointer);

                float rayMinDistance = float.MaxValue;
                int index = 0;

                for (int p = 0; p < 4; p++)
                {
                    if (planes[p].Raycast(ray, out float distance) && distance < rayMinDistance)
                    {
                        rayMinDistance = distance;
                        index = p;
                    }
                }


                rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toPointer.magnitude);
                Vector3 worldPosition = ray.GetPoint(rayMinDistance);
                Vector3 screenPosition = _camera.WorldToScreenPoint(worldPosition);
                Quaternion rotation = GetPointerRotation(index);

                if (toPointer.magnitude > rayMinDistance)
                {
                    pointerView.Show();
                }
                else
                {
                    if (_isAlwaysShown)
                    {
                        pointerView.SetPositionAndRotation(screenPosition, GetPointerRotation(2));
                        return;
                    }
                    pointerView.Hide();
                }

                pointerView.SetPositionAndRotation(screenPosition, rotation);
            }
        }

        public void Add(Pointer pointer)
        {
            if (pointer != null)
            {
                if (_pointersMap.ContainsKey(pointer) == false)
                {
                    if (_pointerViewPrefab != null)
                    {
                        PointerView newPointerView = Instantiate(_pointerViewPrefab, transform);
                        newPointerView.SetIcon(pointer.Icon);
                        _pointersMap.Add(pointer, newPointerView);
                    }
                }
            }
        }

        public void Remove(Pointer pointer)
        {
            if (_pointersMap.ContainsKey(pointer))
            {
                Destroy(_pointersMap[pointer].gameObject);
                _pointersMap.Remove(pointer);
            }
        }

        private Quaternion GetPointerRotation(int planeIndex)
        {
            return planeIndex switch
            {
                0 => Quaternion.Euler(0f, 0f, 90f),
                1 => Quaternion.Euler(0f, 0f, -90f),
                2 => Quaternion.Euler(0f, 0f, 180f),
                3 => Quaternion.Euler(0f, 0f, 0f),
                _ => Quaternion.identity
            };
        }
    }
}