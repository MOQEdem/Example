using System;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Pier _pier;

    private TutorialStatus _status;

    private void Awake()
    {
        _status = new TutorialStatus();
        _status.Load();
    }

    private void OnEnable()
    {
        _pier.PlayerEnter += CompliteTutorial;
    }

    private void OnDisable()
    {
        _pier.PlayerEnter -= CompliteTutorial;
    }

    private void CompliteTutorial(bool isHub)
    {
        _status.SetTutorialAsComplite();
        _status.Save();
    }
}

[Serializable]
public class TutorialStatus : SavedObject<TutorialStatus>
{
    private const string SaveKey = nameof(TutorialStatus);

    [SerializeField] private bool _isTutorialComplite = false;

    public bool IsTutorialComplite => _isTutorialComplite;

    public void SetTutorialAsComplite()
    {
        _isTutorialComplite = true;
        Save();
    }

    public TutorialStatus()
        : base(SaveKey)
    { }

    protected override void OnLoad(TutorialStatus loadedObject)
    {
        _isTutorialComplite = loadedObject.IsTutorialComplite;
    }
}
