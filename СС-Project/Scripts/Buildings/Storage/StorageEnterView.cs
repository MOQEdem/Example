public class StorageEnterView : InteractiveZoneView
{
    protected override void SetValue()
    {
        Value.text = $"{Zone.Resources.Count}/{Zone.TargetCount}";
    }
}
