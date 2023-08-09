using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    private Button _battleButton;

    public Button Button => _battleButton;

    private void Awake()
    {
        _battleButton = GetComponent<Button>();
    }
}
