public class BuffZone : InteractiveZone
{
    protected override void Activate() { }

    public Resource SpendResource()
    {
        if (Resources.Count > 0)
        {
            var resource = Resources[Resources.Count - 1];

            resource.SetParent(null);
            Resources.Remove(resource);
            ChangeCount?.Invoke();
            CheckResourceCount();
            ChangeState(false);

            return resource;
        }

        return null;
    }

    public void SetNewCapacity(int value)
    {
        SetTargetCountResource(value);
        ChangeCount?.Invoke();
    }
}
