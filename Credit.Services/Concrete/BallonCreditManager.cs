using Credit.Core.Utilities.Results.Abstract;
using Credit.Core.Utilities.Results.ComplexTypes;
using Credit.Core.Utilities.Results.Concrete;
using Credit.Entities.Concrete;
using Credit.Entities.Dtos;
using Credit.Services.Abstract;

namespace Credit.Services.Concrete
{
    public class BallonCreditManager : IBallonCreditService
    {
        public IDataResult<CalcCreditListDto> GetCalcBallonCredit(double amount, uint expiry, double interest, int InstallmentAmount)
        {
            // Balon Kredi hesaplama formülü
            // Bu kredi türünde aylık taksit oranını kullanıcı belirler
            // Aylık taksit oranı ilk anaparanın faizinden küçük olmamalıdır
            // Teori : mountlyInterest = (interest/100) * amount
            // InstallmentAmount > mountlyInterest

            //Faizi 100' e böl
            interest = interest / 100;

            double installment = InstallmentAmount;

            double calcMountyAmount = interest * amount;
            //Aylık taksit tutarı aylık faizden küçük ise hata döndür
            if (InstallmentAmount < calcMountyAmount)
            {
                return new DataResult<CalcCreditListDto>(ResultStatus.Success, statusCode: 200, message: $"Aylık Taksit tutarı aylık faiz tutarından({calcMountyAmount}) büyük olmalıdır",null);
            }

            List<CalcCredit> credit = new List<CalcCredit>();

            //Toplam Ödenecek Ve Toplam Faiz
            double totalAmount = 0;
            double totalInterest = 0;

            //Taksit İçerisindeki faiz ve anapara
            double calcInterest = 0;
            double calcBalance = 0;

            //Taksitin Ödenemesi gereken tarih
            DateTime date = DateTime.Now;

            for (int i = 1; i <= expiry; i++)
            {
                calcMountyAmount = interest * amount;

                //Son Taksit Tutarı
                if (i == expiry)
                {
                    //Taksit Tutarını tüm kalan tutar olarak ayarla
                    installment = amount;
                    calcBalance = amount;
                }
                // Aylık Anapara Tutarı
                calcBalance = InstallmentAmount - calcMountyAmount;
                calcInterest = amount * interest;

                totalInterest += calcInterest;
                totalAmount += installment;

                amount = amount - calcBalance;

                credit.Add(
                    new CalcCredit
                    {
                        Number = i, //Taksit No
                        Date = date.AddMonths(1),// Taksit ödeme tarihi
                        Installment = installment, //Taksit tutarı
                        MainBalance = calcBalance, //Taksit içerisindeki anapara
                        Interest = calcInterest, // Taksit İçerisindeki faiz
                        AvailableBalance = amount, // Kalan bakiye
                    });
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
