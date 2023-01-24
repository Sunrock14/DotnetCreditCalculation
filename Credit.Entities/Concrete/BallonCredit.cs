using Credit.Entities.Abstract;

namespace Credit.Entities.Concrete
{
    public class BallonCredit : CreditBase
    {
        public int InstallmentAmount { get; set; }
    }
}
