using System;
using UnityEngine;

public class Target : MonoBehaviour
{}

[Serializable]
public enum TargetType
{
    Player,
    NPC,
    Enemy,
    WoodResource,
    RockResourse,
    NonIntarective
}