namespace Credit.Entities.Abstract
{
    public abstract class CreditBase
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreditModel { get; set; }
        public double Amount { get; set; }
        public int Expiry { get; set; }
        public double Interest { get; set; }
        public int PaymentFrequency { get; set; }
    }
}
