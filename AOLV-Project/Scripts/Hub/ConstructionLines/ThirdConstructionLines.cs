public class ThirdConstructionLines : ConstructionLine
{
    protected override ConstructionPhase InitPhase() => new ThirdConstructionLinesPhase();

    public class ThirdConstructionLinesPhase : ConstructionPhase
    {
        private const string SaveKey = nameof(ThirdConstructionLinesPhase);

        public ThirdConstructionLinesPhase()
            : base(SaveKey)
        { }
    }
}
