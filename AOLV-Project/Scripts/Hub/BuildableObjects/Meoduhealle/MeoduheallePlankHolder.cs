using UnityEngine;

public class MeoduheallePlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new MeoduheallePlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class MeoduheallePlank : Resource
    {
        private const string SaveKey = nameof(MeoduheallePlank);

        public MeoduheallePlank()
            : base(SaveKey, ResourceType.Wood)
        { }
    }
}
