public class FirstConstructionLines : ConstructionLine
{
    protected override ConstructionPhase InitPhase() => new FirstConstructionLinesPhase();

    public class FirstConstructionLinesPhase : ConstructionPhase
    {
        private const string SaveKey = nameof(FirstConstructionLinesPhase);

        public FirstConstructionLinesPhase()
            : base(SaveKey)
        { }
    }
}
