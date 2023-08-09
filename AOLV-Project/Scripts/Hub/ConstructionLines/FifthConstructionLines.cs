public class FifthConstructionLines : ConstructionLine
{
    protected override ConstructionPhase InitPhase() => new FifthConstructionLinesPhase();

    public class FifthConstructionLinesPhase : ConstructionPhase
    {
        private const string SaveKey = nameof(FifthConstructionLinesPhase);

        public FifthConstructionLinesPhase()
            : base(SaveKey)
        { }
    }
}
