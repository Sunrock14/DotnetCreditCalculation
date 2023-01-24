using Credit.Entities.Abstract;

namespace Credit.Entities.Concrete
{
    public class GrowingCredit : CreditBase
    {
        public int IncreaseStartInstallmentNumber { get; set; }
        public int IncreaseFrequency { get; set; }
        public int IncreaseAmount { get; set; }
        public int PercentageOfIncrease { get; set; }
    }
}
