using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ResourcesData", menuName = "GameAssets/ResourcesData")]
public class ResourcesData : ScriptableObject
{
    [SerializeField] private ResourceData[] _data;

    public ResourceData[] Data => _data;

    public Sprite GetIcon(ResourceType type)
    {
        foreach (var resource in _data)
        {
            if (resource.Type == type)
            {
                return resource.Icon;
            }
        }

        return null;
    }

    public ResourceModel GetResourceModel(ResourceType type)
    {
        foreach (var resource in _data)
        {
            if (resource.Type == type)
            {
                return resource.Model;
            }
        }

        return null;
    }
}

[Serializable]
public class ResourceData
{
    [SerializeField] private ResourceType _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private ResourceModel _model;

    public ResourceType Type => _type;
    public Sprite Icon => _icon;
    public ResourceModel Model => _model;
}

