using Credit.Core.Utilities.Results.Abstract;
using Credit.Core.Utilities.Results.ComplexTypes;
using Credit.Core.Utilities.Results.Concrete;
using Credit.Entities.Concrete;
using Credit.Entities.Dtos;
using Credit.Services.Abstract;

namespace Credit.Services.Concrete
{
    public class EqualCreditManager : IEqualCreditService
    {
        public IDataResult<CalcCreditListDto> CalcEqualCredit(double amount, int expiry, double interest)
        {
            ///Kredi hesaplama formülü
            ///PV = Anapara
            ///PMT = taksit
            /// PV = PMT * ((1 + faiz)^vade -1) / ((1 + faiz)^vade * faiz
            /// 

            //Faizi 100' e böl
            interest = interest / 100;

            //Taksiti hesapla
            double PV =
                (Math.Pow((1 + interest), expiry) - 1)
                / (Math.Pow((1 + interest), expiry) * interest);
            double PMT = amount / PV;

            //Toplam Ödenecek Ve Toplam Faiz
            double totalAmount = 0;
            double totalInterest = 0;

            //Taksit İçerisindeki faiz ve anapara
            double calcInterest = 0;
            double calcBalance = 0;

            List<CalcCredit> credit = new List<CalcCredit>();

            //Taksitin Ödenemesi gereken tarih
            DateTime date = DateTime.Now;

            for (int i = 1; i <= expiry; i++)
            {
                //Taksit içerisindeki faiz tutarı
                calcInterest = amount * interest;
                totalInterest += calcInterest;

                //Taksit içerisindeki anapara tutarı
                calcBalance = PMT - calcInterest;

                //Verilen krediden taksit içerisindeki anapara tutarını çıkarıyoruz
                amount = amount - calcBalance;

                // Toplam Ödenecek Para
                totalAmount += PMT;

                credit.Add(
                    new CalcCredit
                    {
                        Number = i, //Taksit No
                        Date = date.AddMonths(1),// Taksit ödeme tarihi
                        Installment = Math.Round(PMT,2), //Taksit tutarı
                        MainBalance = Math.Round(calcBalance,2), //Taksit içerisindeki anapara
                        Interest = Math.Round(calcInterest,2), // Taksit İçerisindeki faiz
                        AvailableBalance = Math.Round(amount,2) // Kalan bakiye
                    });
            }

            return new DataResult<CalcCreditListDto>(ResultStatus.Success, statusCode: 200, new CalcCreditListDto
            {
                CalcCredits = credit,
                TotalAmount = Math.Round(totalAmount,2),
                TotalInterest = Math.Round(totalInterest,2),
                ResultStatus = ResultStatus.Success,
                Message = "İşlem Başarılı!"
            });
        }
    }
}
