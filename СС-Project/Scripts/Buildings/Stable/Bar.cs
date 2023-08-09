using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Image _filler;

    public void SetFill(float normalizedValue) => 
        _filler.fillAmount = normalizedValue;
}
