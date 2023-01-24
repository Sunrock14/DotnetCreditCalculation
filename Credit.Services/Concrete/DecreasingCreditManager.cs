using Credit.Core.Utilities.Results.Abstract;
using Credit.Core.Utilities.Results.ComplexTypes;
using Credit.Core.Utilities.Results.Concrete;
using Credit.Entities.Concrete;
using Credit.Entities.Dtos;
using Credit.Services.Abstract;

namespace Credit.Services.Concrete
{
    public class DecreasingCreditManager : IDecreasingCreditService
    {
        public IDataResult<CalcCreditListDto> CalcDecreasingCredit(double amount, int expiry, double interest, int DecreaseStartingInstallmentNumber,
            int DecreaseFrequency, int? DecreaseAmount, int? DecreasePercentage)
        {
            {
                // Azalan Kredi Formülü
                // Toplam kredi tutarını vadeye bölünüyor ve taksit içerisindeki anapara bulunuyor
                // Anapara ve faiz toplanıyor ve taksit tutarı bulunuyor                

                //Yüzdeli ve Tutar kontrolu yapılıyor
                if (DecreaseAmount == null && DecreasePercentage == null || DecreaseAmount == 0 && DecreasePercentage == 0)
                {
                    return new DataResult<CalcCreditListDto>(ResultStatus.Error, statusCode: 200, new CalcCreditListDto
                    {
                        ResultStatus = ResultStatus.Error,
                        Message = $"Artış tutarı veya Artış yüzdesi ikisi birden boş bırakılamaz."
                    });
                }
                //||  DecreasePercentage != null && DecreaseAmount != 0 && DecreaseAmount != null
                if (DecreaseAmount != null && DecreasePercentage != 0 && DecreasePercentage != null)
                {
                    if (DecreasePercentage != null && DecreaseAmount != 0 && DecreaseAmount != null)
                    {
                        return new DataResult<CalcCreditListDto>(ResultStatus.Error, statusCode: 200, new CalcCreditListDto
                        {
                            ResultStatus = ResultStatus.Error,
                            Message = $"Artış tutarı veya Artış yüzdesinden yalnızca biri seçilebilir."
                        });
                    }

                }

                //Faizi 100' e böl
                interest = interest / 100;

                // Taksit tutarını hesaplama için azalma miktarının toplamını bul
                // Örnek: ilk taksitten itibaren 1000 tl düşer ve vade 12 ay ise 1000 + 2000 + 3000 + 4000 ... + 12000 = 13000 * 78000 toplam azalma miktarı
                double? totalDecreaseAmount = 0;
                int frequency = expiry - DecreaseStartingInstallmentNumber;
                for (int i = 1; i <= frequency; i++)
                {
                    // Belli bir tutar girilmiş ise
                    if (DecreaseAmount != null)
                    {
                        if (i % DecreaseFrequency == 0)
                        {
                            totalDecreaseAmount += i * DecreaseAmount;                                              
                        }
                    }
                    // Yüzdelik tutar girilmiş ise
                    else
                    {
                        if (i % DecreaseFrequency == 0)
                        {
                            totalDecreaseAmount += (i * DecreaseAmount) / 100;
                        }
                    }

                }
                // Taksit tutarının taksitteki faiz tutarından az olup olmadığı kontrol ediliyor
                double installment = (totalDecreaseAmount / expiry) ?? 0;
                double insterestResult = amount * interest;
                if (installment < insterestResult)
                {
                    return new DataResult<CalcCreditListDto>(ResultStatus.Success, statusCode: 200, message: $"Taksit tutarı faiz oranından küçük olamaz.", null);

                }

                //Toplam Ödenecek Ve Toplam Faiz
                double totalAmount = 0;
                double totalInterest = 0;

                //Taksit İçerisindeki faiz ve anapara
                double calcInterest = 0;
                double calcBalance = 0;


                //Taksitin Ödenemesi gereken tarih
                DateTime date = DateTime.Now;
                List<CalcCredit> credit = new List<CalcCredit>();

                for (int i = 1; i <= expiry; i++)
                {
                    //Taksit içerisindeki faiz tutarı
                    calcInterest = amount * interest;
                    totalInterest += calcInterest;

                    //Taksit içerisindeki anapara tutarı
                    calcBalance = installment - calcInterest;

                    //Verilen krediden taksit içerisindeki anapara tutarını çıkarıyoruz
                    // Fakat asla null olmayacak 0 değerleri girilmediği sürece
                    amount = amount - calcBalance;

                    // Toplam Ödenecek Para
                    totalAmount += installment;

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
}

