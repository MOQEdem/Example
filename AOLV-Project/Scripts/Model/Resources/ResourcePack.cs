public class ResourcePack
{
    private int _value;
    private ResourceType _type;

    public int Value => _value;
    public ResourceType Type => _type;

    public ResourcePack(int value, ResourceType type)
    {
        _value = value;
        _type = type;
    }
}
