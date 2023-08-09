public class EnemySquad : Squad
{
    public void Destroy(NPC[] units)
    {
        foreach (NPC npc in units)
            if (!npc.IsDead)
                npc.Destroy();
    }
}
