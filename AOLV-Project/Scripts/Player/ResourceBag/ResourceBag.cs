using System;
using Agava.YandexMetrica;
using UnityEngine;
using UnityEngine.Events;

public class ResourceBag : MonoBehaviour
{
    [SerializeField] private ResourcesData _data;
    [SerializeField] private BagMessageUI _bagMessageUI;
    [SerializeField] private ResourceBagHolder _holder;

    [SerializeField] private ResourceHolder[] _resources;
    private bool _isInSpendingProcess = false;

    public bool InSpendingProcess => _isInSpendingProcess;

    public event UnityAction<ResourcePack, Transform> ResourceSpended;
    public event UnityAction<ResourcePack, Transform> ResourceTaken;
    public event UnityAction HolderFilled;
    public event UnityAction<ResourceType> HolderEmpty;

    private void Awake()
    {
        if (_holder != null)
            _resources = _holder.GetComponentsInChildren<ResourceHolder>();
    }

    public void TakeResource(ResourcePack resourcePack, Transform takingPoint)
    {
        if (resourcePack.Value <= 0)
            return;

        foreach (var resource in _resources)
        {
            if (resource.Type == resourcePack.Type)
            {
                resource.Add(resourcePack.Value);

                if (resource.Value >= resource.MaxValue)
                {
                    HolderFilled?.Invoke();
                }

                if (_bagMessageUI != null)
                    SendMessage(resourcePack, true, takingPoint.position);

                ResourceTaken?.Invoke(resourcePack, takingPoint);
            }
        }
    }

    public int SpendResources(ResourcePack resourcePack, Transform spendingPoint)
    {
        foreach (var resource in _resources)
        {
            if (resource.Type == resourcePack.Type)
            {
                int possibleSpending = Mathf.Min(resourcePack.Value, resource.Value);

                resource.Spend(possibleSpending);

                if (resource.Value == 0)
                    HolderEmpty?.Invoke(resource.Type);

                if (possibleSpending > 0)
                {
                    ResourceSpended?.Invoke(resourcePack, spendingPoint);

                    if (_bagMessageUI != null)
                        if (!_isInSpendingProcess)
                            SendMessage(new ResourcePack(possibleSpending, resource.Type), false, this.transform.position);
                }

                return possibleSpending;
            }
        }
        return 0;
    }

    public bool IsAbleToSpendResource(ResourcePack resourcePack)
    {
        foreach (var resource in _resources)
        {
            if (resource.Type == resourcePack.Type)
            {
                if (resource.Value == 0)
                    HolderEmpty?.Invoke(resource.Type);
                
                if (resource.Value >= resourcePack.Value)
                    return true;
            }
        }
        return false;
    }

    public bool IsAbleToTakeResource(ResourcePack resourcePack, out int possibleToAdd)
    {
        possibleToAdd = 0;
        foreach (var resource in _resources)
        {
            if (resource.Type == resourcePack.Type)
            {
                if (!resource.IsHaveLimit)
                {
                    possibleToAdd = int.MaxValue;
                    return true;
                }

                if (resource.Value < resource.MaxValue)
                {
                    possibleToAdd = resource.MaxValue - resource.Value;
                    return true;
                }
                else
                {
                    if (_bagMessageUI != null)
                        SendMessage(resourcePack, true, this.transform.position);

                    HolderFilled?.Invoke();
                }
            }
        }
        return false;
    }

    public void SetSpendingStatus(bool isSpending)
    {
        _isInSpendingProcess = isSpending;
    }

    private void SendMessage(ResourcePack resourcePack, bool isResourceAdded, Vector3 point)
    {
        var message = Instantiate(_bagMessageUI, point, Quaternion.identity);

        message.SetMessage(resourcePack.Value, isResourceAdded, _data.GetIcon(resourcePack.Type));
    }
}
