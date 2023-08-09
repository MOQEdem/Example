public class BuildZoneView : InteractiveZoneView
{
    protected override void SetValue()
    {
        Value.text = (Zone.TargetCount - Zone.Resources.Count).ToString();
        Progressbar.fillAmount = (float)Zone.Resources.Count / Zone.TargetCount;
    }
}
