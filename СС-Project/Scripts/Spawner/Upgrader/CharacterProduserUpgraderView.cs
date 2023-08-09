using TMPro;
using UnityEngine;

public class CharacterProduserUpgraderView : InteractiveZoneView
{
    [SerializeField] private TMP_Text _fullFillText;
    [SerializeField] private CanvasGroup _ui;

    private void Start()
    {
        _fullFillText.gameObject.SetActive(false);
    }

    protected override void SetValue()
    {
        if (Zone.TargetCount != 0)
        {
            int value = Zone.TargetCount - Zone.Resources.Count;

            if (value != 0)
            {
                if (!Value.gameObject.activeSelf)
                {
                    Value.gameObject.SetActive(true);
                    _fullFillText.gameObject.SetActive(false);
                }

                Value.text = value.ToString();
                Progressbar.fillAmount = (float)Zone.Resources.Count / Zone.TargetCount;
                _ui.alpha = 1f;
            }
            else
            {
                if (Value.gameObject.activeSelf)
                {
                    Value.gameObject.SetActive(false);
                    _fullFillText.gameObject.SetActive(true);
                }

                Progressbar.fillAmount = 0;
                _ui.alpha = 0f;
            }
        }
        else
        {
            _ui.alpha = 0f;
        }
    }
}
