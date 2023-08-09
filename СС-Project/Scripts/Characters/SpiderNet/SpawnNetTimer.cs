using UnityEngine;

public class SpawnNetTimer : MonoBehaviour
{
    [SerializeField] private SpiderNetSpawner _netSpawner;
    [SerializeField] [Min(0)] private float _interval = 1f;

    private Character _character;
    private float _timer;

    private void Awake() => 
        _character = GetComponent<Character>();

    private void OnEnable() => 
        _character.Died += OnDied;

    private void OnDisable() => 
        _character.Died -= OnDied;

    private void OnDied(Character character)
    {
        character.Died -= OnDied;
        enabled = false;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _interval)
        {
            _netSpawner.ThrowNetNear();
            _timer = 0;
        }
    }
}
