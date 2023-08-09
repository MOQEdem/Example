public class SecondConstructionLines : ConstructionLine
{
    protected override ConstructionPhase InitPhase() => new SecondConstructionLinesPhase();

    public class SecondConstructionLinesPhase : ConstructionPhase
    {
        private const string SaveKey = nameof(SecondConstructionLinesPhase);

        public SecondConstructionLinesPhase()
            : base(SaveKey)
        { }
    }
}
