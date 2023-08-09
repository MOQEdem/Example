public class OnePlayerPositionHolder : PlayerPositionHolder
{
    private string _heapName = nameof(OnePlayerPosition);

    protected override PlayerPosition initpPlayerPosition()
    {
        return new PlayerPosition(_heapName);
    }
}

public class OnePlayerPosition : PlayerPosition
{
    private const string _saveKey = nameof(OnePlayerPosition);
    
    public OnePlayerPosition() : base(_saveKey){}
}