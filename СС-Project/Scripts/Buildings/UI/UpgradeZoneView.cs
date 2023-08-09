using TMPro;
using UnityEngine;

public class UpgradeZoneView : InteractiveZoneView
{
    [SerializeField] private TMP_Text _fullFillText;

    protected override void SetValue()
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
        }
        else
        {
            if (Value.gameObject.activeSelf)
            {
                Value.gameObject.SetActive(false);
                _fullFillText.gameObject.SetActive(true);
            }

            Progressbar.fillAmount = 0;
        }
    }
}
