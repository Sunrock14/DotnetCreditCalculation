using Credit.Entities.Abstract;

namespace Credit.Entities.Concrete
{
    public class InterimPaymentCredit : CreditBase
    {
        public int FirstPaymentNo { get; set; }
        public int InterimPaymentFrequency { get; set; }
        public int InterimPaymentAmount { get; set; }
        public bool IsAddInterimAmount { get; set; }
    }
}
