using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ForgeUpgraderStages", menuName = "GameAssets/ForgeUpgraderStages")]
public class ForgeUpgraderStages : ScriptableObject
{
    [SerializeField] private ForgeUpgraderStage[] _stages;

    public ForgeUpgraderStage[] Stages => _stages;
}

[Serializable]
public class ForgeUpgraderStage
{
    [SerializeField] private UpgraderLevel _level;
    [SerializeField] private ForgeRecource _resorce;
    [SerializeField] private float _time;
    [SerializeField] private int _capacity;
    [SerializeField] private Vector2 _materialOffset;

    public UpgraderLevel Level => _level;
    public ForgeRecource Resorce => _resorce;
    public float Time => _time;
    public int Capacity => _capacity;
    public Vector2 MaterialOffset => _materialOffset;
}
