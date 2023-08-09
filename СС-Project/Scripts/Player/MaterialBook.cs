using UnityEngine;

[CreateAssetMenu(fileName = "MaterialBook", menuName = "GameAssets/MaterialBook")]
public class MaterialBook : ScriptableObject
{
    [SerializeField] private Material[] _materials;

    public Material GetMaterial(int materialLevel)
    {
        if (materialLevel < _materials.Length)
            return _materials[materialLevel];
        else
            return _materials[_materials.Length - 1];
    }
}
