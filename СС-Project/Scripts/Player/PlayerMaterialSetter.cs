using UnityEngine;

public class PlayerMaterialSetter : MonoBehaviour
{
    [SerializeField] private MeshRenderer _swordRenderer;
    [SerializeField] private SkinnedMeshRenderer[] _armorRenderer;
    [SerializeField] private MaterialBook _swordMaterialBook;
    [SerializeField] private MaterialBook _armorMaterialBook;

    public void SetSwordMaterial(int materialLevel)
    {
        _swordRenderer.material = _swordMaterialBook.GetMaterial(materialLevel);
    }

    public void SetArmorMaterial(int materialLevel)
    {
        Material material = _armorMaterialBook.GetMaterial(materialLevel);

        foreach (var armor in _armorRenderer)
            armor.material = material;
    }
}
