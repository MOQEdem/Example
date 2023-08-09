public class GhastBall : Arrow
{
    private TargetDetector _targetDetector;

    private void Awake()
    {
        _targetDetector = GetComponentInChildren<TargetDetector>();
    }

    protected override void DoDamage(int damage)
    {
        Character[] targets = _targetDetector.Targets.ToArray();

        foreach (var npc in targets)
        {
            if (npc != null && !npc.IsDead)
            {
                npc.ApplyDamage(damage);
            }
        }
    }
}
