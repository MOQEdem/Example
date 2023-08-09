using System.Collections;
using UnityEngine;

public class GroundSwitcher : MonoBehaviour
{
    [SerializeField] private GroundSwitcherType _type;

    private float _preparationTime = 1f;
    private Character _npc;
    private bool _isPreparationComplite = false;

    public GroundSwitcherType Type => _type;

    private void Awake()
    {
        _npc = GetComponentInParent<Character>();

        transform.localPosition += new Vector3(0, UnityEngine.Random.Range(-0.3f, 0), 0);
    }

    private void OnEnable()
    {
        _npc.Died += OnNPCDied;
    }

    private void OnDisable()
    {
        _npc.Died -= OnNPCDied;
    }

    private void Start()
    {
        StartCoroutine(PreparingGround());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ground>(out Ground ground))
        {
            if (_isPreparationComplite)
            {
                ground.SetSwitcherStatus(true, this);
            }
            else
            {
                ground.SetGameStartStatus(this, true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Ground>(out Ground ground))
        {
            if (_isPreparationComplite)
            {
                ground.SetSwitcherStatus(false, this);
            }
            else
            {
                ground.SetGameStartStatus(this, false);
            }
        }
    }

    private void OnNPCDied(Character npc)
    {
        var collider = GetComponent<BoxCollider>();
        collider.size = Vector3.zero;
    }
    private IEnumerator PreparingGround()
    {
        yield return new WaitForSeconds(_preparationTime);
        _isPreparationComplite = true;
    }
}

public enum GroundSwitcherType
{
    Magma,
    Coal
}
