public class ResourceExchangeEnter : InteractiveZone
{
    protected override void Activate() { }

    public void SpendResource()
    {
        if (Resources.Count > 0)
        {
            Resources[Resources.Count - 1].SetParent(null);
            Resources.RemoveAt(Resources.Count - 1);
            ChangeCount?.Invoke();
            CheckResourceCount();
            ChangeState(false);
        }
    }
}
