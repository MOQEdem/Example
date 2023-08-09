using UnityEngine;

public class RidingTransport : MonoBehaviour
{
    [SerializeField] private Transform _saddle;
    [SerializeField] private RidingTransportAnimator _ridingTransportAnimator;
    [SerializeField] [Min(0)] private float _speed = 6;
    [SerializeField] [Min(0)] private int _healthBonus = 50;

    public Transform Saddle => _saddle;
    public RidingTransportAnimator Animator => _ridingTransportAnimator;
    public float Speed => _speed;
    public int HealthBonus => _healthBonus;
}
