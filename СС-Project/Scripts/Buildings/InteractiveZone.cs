using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveZone : MonoBehaviour
{
    [SerializeField] protected ResourceType _resourceType;
    [SerializeField] protected int _startCount;
    [SerializeField] protected Transform _resourceStack;

    private bool _isFilled;
    private List<Resource> _resources = new List<Resource>();

    public int TargetCount => _startCount;
    public List<Resource> Resources => _resources;
    public bool IsFilled => _isFilled;
    public ResourceType ResourceType => _resourceType;

    public Action Ended;
    public Action NextUpgrade;
    public Action ChangeCount;

    protected virtual void Start()
    {
        ChangeCount?.Invoke();
    }

    public bool CheckResourceCount()
    {
        if (_resources.Count != TargetCount)
        {
            ChangeState(false);
            return true;
        }
        else
        {
            ChangeState(true);

            if (TargetCount != 0)
                Activate();

            return false;
        }
    }

    protected abstract void Activate();

    protected void SetTargetCountResource(int count)
    {
        _startCount = count;
        _isFilled = false;
    }

    protected void ResetResourceStack()
    {
        _resources = new List<Resource>();
    }

    public virtual void ApplyResource(Resource resource, bool isNeedAnimation)
    {
        if (resource != null && CheckResourceCount())
        {
            _resources.Add(resource);
            resource.SetParent(_resourceStack);

            if (isNeedAnimation)
                resource.SetTarget(Vector3.zero);
            else
                resource.transform.localPosition = Vector3.zero;

            ChangeCount?.Invoke();
            CheckResourceCount();
        }
    }

    public void LoadResorces(Resource resource)
    {
        _resources.Add(resource);
        resource.SetParent(_resourceStack);

        resource.transform.localPosition = Vector3.zero;
    }

    public void ChangeState(bool isFilled)
    {
        _isFilled = isFilled;
    }
}
