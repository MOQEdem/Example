public class StoreWarehouseView : InteractiveZoneView
{
    protected override void SetValue()
    {
        Value.text = $"{Zone.Resources.Count} / {Zone.TargetCount} ";
        Progressbar.fillAmount = (float)Zone.Resources.Count / Zone.TargetCount;
    }
}
