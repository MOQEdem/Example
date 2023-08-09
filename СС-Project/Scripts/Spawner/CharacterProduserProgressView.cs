using UnityEngine;
using UnityEngine.UI;

public class CharacterProduserProgressView : MonoBehaviour
{
    [SerializeField] private Image _bar;

    public void SetProgressValue(float value)
    {
        _bar.fillAmount = value;
    }
}
