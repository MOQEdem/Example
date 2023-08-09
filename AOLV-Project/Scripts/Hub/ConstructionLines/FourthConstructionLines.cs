public class FourthConstructionLines : ConstructionLine
{
    protected override ConstructionPhase InitPhase() => new FourthConstructionLinesPhase();

    public class FourthConstructionLinesPhase : ConstructionPhase
    {
        private const string SaveKey = nameof(FourthConstructionLinesPhase);

        public FourthConstructionLinesPhase()
            : base(SaveKey)
        { }
    }
}
