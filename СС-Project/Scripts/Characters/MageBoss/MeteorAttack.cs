using UnityEngine;

public class MeteorAttack : MonoBehaviour
{
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private Meteor _meteorPrefab;
    [SerializeField] [Min(0)] private int _damage = 30;

    [ContextMenu("Cast")]
    public void Cast()
    {
        foreach (Character target in _targetDetector.Targets)
        {
            Meteor meteor = Instantiate(_meteorPrefab, target.transform.position, Quaternion.identity);
            meteor.Move(()=>
            {
                target?.ApplyDamage(_damage);
            });
        }
    }
}
