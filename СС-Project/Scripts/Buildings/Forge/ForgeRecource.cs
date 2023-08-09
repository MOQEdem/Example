using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ForgeRecource : MonoBehaviour
{
    [Header("BuffData")]
    [SerializeField] private int _additionalHealth;
    [SerializeField] private int _additionalDamage;
    [SerializeField] private float _additionalScale;
    [SerializeField] private Material _meleeArmorMaterial;
    [SerializeField] private Material _rangedArmorMaterial;
    [Space]
    [Header("RecourceData")]
    [SerializeField] private GameObject _hotObject;
    [SerializeField] private GameObject _coldMaterial;
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _box;

    private Material _hotMaterial;
    private Material _boxMaterial;

    private Coroutine _movingToBuffNPC;
    private Coroutine _dissolvingMaterial;

    private void Awake()
    {
        _hotMaterial = _hotObject.GetComponent<MeshRenderer>().material;
        _boxMaterial = _box.GetComponent<MeshRenderer>().material;
        _sword.gameObject.SetActive(false);
    }

    public void StartSetHotMaterial(float time)
    {
        if (_dissolvingMaterial == null)
            _dissolvingMaterial = StartCoroutine(DissolvingMaterial(_hotMaterial, time));
    }

    public void SetSword()
    {
        _hotObject.gameObject.SetActive(false);
        _coldMaterial.gameObject.SetActive(false);
        _sword.gameObject.SetActive(true);
    }

    public void Pack(float time)
    {
        if (_dissolvingMaterial == null)
            _dissolvingMaterial = StartCoroutine(DissolvingMaterial(_boxMaterial, time));
    }

    public void BuffNPC(NPC npc)
    {
        if (_movingToBuffNPC == null)
            _movingToBuffNPC = StartCoroutine(MovingToBuffNPC(npc));
    }

    private IEnumerator MovingToBuffNPC(NPC npc)
    {
        if (npc is NPCWalker walker)
        {
            float animationTime = 0.7f;

            Tween move = transform.DOJump(npc.transform.position, 1, 1, animationTime);
            yield return move.WaitForCompletion();

            walker.ChangeSkill(_additionalHealth, _additionalDamage, _additionalScale);

            if (npc is PlayerCharacter character)
                character.ShowArmor(_meleeArmorMaterial);

            if (npc is PlayerArcher archer)
                archer.ShowArmor(_rangedArmorMaterial);

            walker.MoveToStartPosition();

            Destroy(this.gameObject);
        }
    }

    private IEnumerator DissolvingMaterial(Material material, float time)
    {
        float currentDissolveValue = 1f;

        while (material.GetFloat("_DissolveAmount") > 0)
        {
            currentDissolveValue -= Time.deltaTime / time;
            material.SetFloat("_DissolveAmount", currentDissolveValue);
            yield return null;
        }

        _dissolvingMaterial = null;
    }
}
