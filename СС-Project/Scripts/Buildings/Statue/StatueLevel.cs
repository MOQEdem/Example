using UnityEngine;

public class StatueLevel : SavedObject<StatueLevel>
{
    private const string SaveKey = nameof(StatueLevel);

    [SerializeField] private int _level = -1;

    public int Level => _level;

    public StatueLevel()
        : base(SaveKey)
    { }

    public void LevelUp()
    {
        _level++;
    }

    protected override void OnLoad(StatueLevel loadedObject)
    {
        _level = loadedObject.Level;
    }
}
