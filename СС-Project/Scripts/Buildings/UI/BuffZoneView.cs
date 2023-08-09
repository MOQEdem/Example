using UnityEngine;

public class BuffZoneView : InteractiveZoneView
{
    [SerializeField] private Transform[] _stackPoints;
    [SerializeField] private float _stepOffset;

    private int _lastResourceCount = 0;

    protected override void SetValue()
    {
        Value.text = $"{Zone.Resources.Count} / {Zone.TargetCount} ";
        Progressbar.fillAmount = (float)Zone.Resources.Count / Zone.TargetCount;



        if (Zone.Resources.Count > 0 && Zone.Resources.Count > _lastResourceCount)
        {
            var currentResource = Zone.Resources[Zone.Resources.Count - 1];
            currentResource.StopAllMovement();
            currentResource.SetTarget(GetCurrentFreePoint());
        }

        _lastResourceCount = Zone.Resources.Count;
    }

    private Vector3 GetCurrentFreePoint()
    {
        int halfOfTargetCount = Zone.TargetCount / 2;

        if (Zone.Resources.Count < halfOfTargetCount)
            return new Vector3(0, Zone.Resources.Count * _stepOffset, 0) + _stackPoints[0].transform.localPosition;
        else
            return new Vector3(0, (Zone.Resources.Count - halfOfTargetCount) * _stepOffset, 0) + _stackPoints[1].transform.localPosition;
    }
}
