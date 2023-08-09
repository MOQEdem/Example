public class FirstHeapHolder : HeapHolder
{
    private string _heapName = nameof(FirstHeap);
    
    protected override Heap InitHeap()
    {
        return new Heap(_heapName);
    }
}

public class FirstHeap : Heap
{
    private const string _saveKey = nameof(FirstHeap);

    public FirstHeap() : base(_saveKey){}
}
