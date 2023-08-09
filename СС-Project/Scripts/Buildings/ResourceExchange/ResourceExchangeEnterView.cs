using UnityEngine;

public class ResourceExchangeEnterView : InteractiveZoneView
{
    [SerializeField] private Transform _stackPoint;
    [SerializeField] private float _stepOffset;

    protected override void SetValue()
    {
        Value.text = $"{Zone.Resources.Count} / {Zone.TargetCount} ";
        Progressbar.fillAmount = (float)Zone.Resources.Count / Zone.TargetCount;

        if (Zone.Resources.Count > 0)
        {
            var currentResource = Zone.Resources[Zone.Resources.Count - 1];
            currentResource.StopAllMovement();
            currentResource.SetTarget(GetCurrentFreePoint());
        }
    }

    private Vector3 GetCurrentFreePoint()
    {
        return new Vector3(0, Zone.Resources.Count * _stepOffset, 0);
    }
}
