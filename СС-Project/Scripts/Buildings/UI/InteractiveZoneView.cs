using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractiveZoneView : MonoBehaviour
{
    [SerializeField] protected TMP_Text Value;
    [SerializeField] protected Image Progressbar;

    protected InteractiveZone Zone;

    private void Awake()
    {
        Zone = GetComponent<InteractiveZone>();
    }

    private void OnEnable()
    {
        Zone.ChangeCount += SetValue;
    }

    private void OnDisable()
    {
        Zone.ChangeCount -= SetValue;
    }

    protected abstract void SetValue();
}
