using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCGroupView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _removeButton;
    [SerializeField] private NPCGroupBuyer _buyer;

    private NPCGroupBuyer[] _allBuyers;

    private void Awake()
    {
        _allBuyers = GetComponents<NPCGroupBuyer>();
    }

    private void OnEnable()
    {
        if (!_buyer.IsNPCUnlocked)
        {
            _buyer.Unlocked += OnNPCUnlocked;
        }

        foreach (NPCGroupBuyer buyer in _allBuyers)
            buyer.Updated += UpdateView;
    }

    private void OnDisable()
    {
        if (_buyer.gameObject.activeSelf)
            if (!_buyer.IsNPCUnlocked)
                _buyer.Unlocked -= OnNPCUnlocked;

        foreach (NPCGroupBuyer buyer in _allBuyers)
            buyer.Updated -= UpdateView;
    }

    private void Start()
    {
        if (!_buyer.IsNPCUnlocked)
        {
            Diactivate();
        }
        else
        {
            Activate();
            _buyer.Unlocked -= OnNPCUnlocked;
        }
    }

    private void UpdateView()
    {
        _text.text = _buyer.CurrentNPCCount.ToString();

        if (_buyer.IsAbleToAdd)
            _addButton.interactable = true;
        else
            _addButton.interactable = false;

        if (_buyer.IsAbleToRemove)
            _removeButton.interactable = true;
        else
            _removeButton.interactable = false;
    }

    private void Activate()
    {
        _canvasGroup.interactable = true;
    }

    private void Diactivate()
    {
        _canvasGroup.interactable = false;
    }

    private void OnNPCUnlocked()
    {
        _buyer.Unlocked -= OnNPCUnlocked;
        Activate();
    }
}
