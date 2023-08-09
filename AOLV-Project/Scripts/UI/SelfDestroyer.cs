using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    [SerializeField] private float _delay;

    void Start()
    {
        Destroy(gameObject, _delay);    
    }
}
