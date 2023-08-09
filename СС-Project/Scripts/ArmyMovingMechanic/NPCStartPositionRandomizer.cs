using UnityEngine;

public class NPCStartPositionRandomizer : MonoBehaviour
{
    [SerializeField] private float _randomizationRadius = 1;

    private void Awake()
    {
        Vector2 pos = Random.insideUnitCircle * _randomizationRadius;

        transform.position += new Vector3(pos.x, 0, pos.y); ;
    }
}
