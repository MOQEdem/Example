public class TherdHeapHolder : HeapHolder
{
    private string _heapName = nameof(TherdHeap);
    
    protected override Heap InitHeap()
    {
        return new Heap(_heapName);
    }
}

public class TherdHeap : Heap
{
    private const string _saveKey = nameof(TherdHeap);

    public TherdHeap() : base(_saveKey){}
}
