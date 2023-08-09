using UnityEngine;
using TMPro;

public class StoreLoteView : MonoBehaviour
{
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private TMP_Text _maxLevelMessage;


    private void Awake()
    {
        _maxLevelMessage.gameObject.SetActive(false);
    }

    public void SetUpgradeCost(float cost)
    {
        _cost.text = cost.ToString();
    }

    public void SetMaxStep()
    {
        _cost.gameObject.SetActive(false);
        _maxLevelMessage.gameObject.SetActive(true);
    }
}
