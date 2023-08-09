public class SecondHeapHolder : HeapHolder
{
    private string _heapName = nameof(SecondHeap);
    
    protected override Heap InitHeap()
    {
        return new Heap(_heapName);
    }
}

public class SecondHeap : Heap
{
    private const string _saveKey = nameof(SecondHeap);

    public SecondHeap() : base(_saveKey){}
}
