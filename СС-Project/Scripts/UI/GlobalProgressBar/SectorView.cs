using UnityEngine;
using UnityEngine.UI;

public class SectorView : MonoBehaviour
{
    [SerializeField] private Image _nonActiveImage;

    public void SetActive(bool isActive) => 
        _nonActiveImage.gameObject.SetActive(isActive);
}
