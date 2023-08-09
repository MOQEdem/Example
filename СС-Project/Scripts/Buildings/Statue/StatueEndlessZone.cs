public class StatueEndlessZone : InteractiveZone
{
    protected override void Activate()
    {

    }

    public override void ApplyResource(Resource resource, bool isNeedAnimation)
    {
        base.ApplyResource(resource, isNeedAnimation);
        Resources.Clear();
    }
}
