using Credit.Core.Utilities.Results.Abstract;
using Credit.Core.Utilities.Results.ComplexTypes;
using Credit.Core.Utilities.Results.Concrete;
using Credit.Entities.Concrete;
using Credit.Entities.Dtos;
using Credit.Services.Abstract;

namespace Credit.Services.Concrete
{
    public class InterimPaymentCreditManager : IInterimPaymentCreditService
    {
        public IDataResult<CalcCreditListDto> GetCalcInterimCredit(double amount, int expiry, double interest,
            int FirstPaymentNo, int InterimPaymentFrequency, int InterimPaymentAmount, bool IsAddInterimAmount)
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

            // Ara ödeme eklendiğinde olan taksitleri hesaplamak için gerekli olan hesaplama
            double calcInterim = 0;
            int frequencyCounter = 0;
            for (int i = 1; i <= expiry; i++)
            {
                if (FirstPaymentNo <= expiry && InterimPaymentFrequency == frequencyCounter)
                {
                    calcInterim += calcInterim + InterimPaymentAmount;
                    frequencyCounter = 0;
                }
                else
                {
                    frequencyCounter++;
                }
            }

            // Vadeye göre ortalamasını al
            calcInterim = calcInterim / expiry;

            // Taksitleri ara ödemeye göre ayarla
            PMT -= PMT - calcInterim;

            int counter = 0;
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
                if (FirstPaymentNo <= expiry && InterimPaymentFrequency == counter)
                {
                    credit.Add(
                        new CalcCredit
                        {
                            Number = i, //Taksit No
                            Date = date.AddMonths(1),// Taksit ödeme tarihi
                            Installment = PMT, //Taksit tutarı
                            MainBalance = calcBalance, //Taksit içerisindeki anapara
                            Interest = calcInterest, // Taksit İçerisindeki faiz
                            AvailableBalance = amount, // Kalan bakiye
                        });

                    counter = 0;
                }
                else
                {
                    credit.Add(
                        new CalcCredit
                        {
                            Number = i, //Taksit No
                            Date = date.AddMonths(1),// Taksit ödeme tarihi
                            Installment = PMT, //Taksit tutarı
                            MainBalance = calcBalance, //Taksit içerisindeki anapara
                            Interest = calcInterest, // Taksit İçerisindeki faiz
                            AvailableBalance = amount, // Kalan bakiye
                        });

                    counter++;
                }
            }

            return new DataResult<CalcCreditListDto>(ResultStatus.Success, statusCode: 200, new CalcCreditListDto
            {
                CalcCredits = credit,
                TotalAmount = totalAmount,
                TotalInterest = totalInterest,
                ResultStatus = ResultStatus.Success,
                Message = "İşlem Başarılı"
            });
        }
    }
}
