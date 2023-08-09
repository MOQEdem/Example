using UnityEngine;
using TMPro;

public class DialogueMessage : MonoBehaviour
{
    [SerializeField] private Sprite _speakerIcon;
    [SerializeField] private bool _isPlayerCharacterMessage;

    private TMP_Text _text;

    public TMP_Text Text => _text;
    public Sprite SpeakerIcon => _speakerIcon;
    public bool IsPlayerCharacterMessage => _isPlayerCharacterMessage;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
}
