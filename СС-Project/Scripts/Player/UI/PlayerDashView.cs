using UnityEngine;
using UnityEngine.UI;

public class PlayerDashView : MonoBehaviour
{
    [SerializeField] private Image _icon;

    public void SetAvailability(bool isAvailable)
    {
        Color currentColor = _icon.color;
        currentColor.a = isAvailable ? 1f : 0.5f;

        _icon.color = currentColor;
    }
}
