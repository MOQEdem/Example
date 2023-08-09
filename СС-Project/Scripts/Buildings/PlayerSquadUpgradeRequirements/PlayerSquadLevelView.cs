using TMPro;
using UnityEngine;

public class PlayerSquadLevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _squadLevel;

    private PlayerSquad _squad;

    private void Awake()
    {
        _squad = GetComponentInParent<PlayerSquad>();
    }

    private void OnEnable()
    {
        _squad.NewLevelSet += OnNewLevelSet;
    }

    private void OnDisable()
    {
        _squad.NewLevelSet -= OnNewLevelSet;
    }

    private void OnNewLevelSet(int level)
    {
        _squadLevel.text = (level + 1).ToString();
    }
}
