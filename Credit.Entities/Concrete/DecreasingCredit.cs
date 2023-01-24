using Credit.Entities.Abstract;

namespace Credit.Entities.Concrete
{
    public class DecreasingCredit : CreditBase
    {
        public int DecreaseStartingInstallmentNumber { get; set; }
        public int DecreaseFrequency { get; set; }
        public int DecreaseAmount { get; set; }
        public int DecreasePercentage { get; set; }
    }
}
