using UnityEngine;

public class SpiderNet : MonoBehaviour
{
    [SerializeField] private SpiderNetMover _mover;

    public SpiderNetMover Mover => _mover;
}
