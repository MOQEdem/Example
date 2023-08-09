using System;
using UnityEngine;

public abstract class IslandHolder : MonoBehaviour
{
    private PlayerTrigger _UI;
    private IslandLoader _pier;
    private ModelSwitch _model;
    private Island _island;

    protected abstract Island InitIsland();

    private void Awake()
    {
        _UI = GetComponentInChildren<PlayerTrigger>();
        _pier = GetComponentInChildren<IslandLoader>();
        _model = GetComponentInChildren<ModelSwitch>();
    }

    private void OnEnable()
    {
        _island = InitIsland();
        _island.Load();
        _pier.Visit += Visit;
        if (_island.Cooldown > 0)
        {
           _model.gameObject.SetActive(false);
           _pier.gameObject.SetActive(false);
           _UI.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _pier.Visit -= Visit;
        _island.Save();
    }

    public void Visit()
    {
        _island.SetCooldown();
        _island.Save();
    }
}