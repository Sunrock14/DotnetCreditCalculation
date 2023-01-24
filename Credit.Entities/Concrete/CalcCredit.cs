namespace Credit.Entities.Concrete
{
    public class CalcCredit
    {
        public int Number { get; set; }

        public DateTime Date { get; set; }

        public double Installment { get; set; }

        public double Interest { get; set; }

        public double AvailableBalance { get; set; }

        public double MainBalance { get; set; }

    }
}
