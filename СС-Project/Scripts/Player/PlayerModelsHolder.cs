using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelsHolder : MonoBehaviour
{
    [SerializeField] List<Player> _models;

    public IEnumerable<Player> Models => _models;
    public int ModelsCount => _models.Count;

    public void DisableAllModels()
    {
        foreach (var model in _models)
            model.gameObject.SetActive(false);
    }

    public void EnableCurrentModel(int index)
    {
        _models[index].gameObject.SetActive(true);
    }

    public Player GetCurrentModel(int index)
    {
        return _models[index];
    }
}
