namespace Monitor.Business.Common
{
    public class CustomerSatisfactionModel
    {
        public decimal VerySatisfied { get; set; }
        public decimal SomehowSatisfied { get; set; }
        public decimal NeitherSatisfiedNorUnsatisfied { get; set; }
        public decimal SomehowUnsatisfied { get; set; }
        public decimal VeryUnsatisfied { get; set; }

        public decimal Total => VerySatisfied + SomehowSatisfied + NeitherSatisfiedNorUnsatisfied + SomehowUnsatisfied + VeryUnsatisfied;
        public decimal Complaints => SomehowUnsatisfied + VeryUnsatisfied;
    }
}
