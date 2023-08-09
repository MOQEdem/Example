using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThertWoodIslandHolder : IslandHolder
{
    private string _islandName = nameof(ThertWoodIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class ThertWoodIsland : Island
{
    private const string _saveKey = nameof(ThertWoodIsland);
    public ThertWoodIsland() : base(_saveKey){}
}