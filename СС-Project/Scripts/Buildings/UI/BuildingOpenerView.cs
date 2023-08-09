using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class BuildingOpenerView : MonoBehaviour
{
    private TMP_Text _text;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetRequiredLevelValue(int level)
    {
        _canvasGroup.alpha = 0f;
        _text.text = $"lvl {level + 1}";
    }
}
