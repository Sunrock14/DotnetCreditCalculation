using Credit.Core.Utilities.Results.Abstract;
using Credit.Entities.Dtos;

namespace Credit.Services.Abstract
{
    public interface IDecreasingCreditService
    {
        /// <summary>
        /// Bu işlem her taksitin belli bir miktarda azalarak hesaplanan kredi modelidir. Yüzdelik olarak veya belli bir miktarda azalmaktadır.
        /// ÖR
        /// 1-15.03.20xx- 3750,45 ₺
        /// 2-15.04.20xx- 3623,69 ₺
        /// 3-15.05.20xx- 3512,98 ₺
        /// ...
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="expiry"></param>
        /// <param name="interest"></param>
        /// <param name="DecreaseStartingInstallmentNumber"></param>
        /// <param name="DecreaseFrequency"></param>
        /// <param name="DecreaseAmount"></param>
        /// <param name="DecreasePercentage"></param>
        /// <returns name="CalcCreditListDto">İstek yapan kullanıcılara dönülen modeldir.Klasik olarak NO-TARİH-TAKSİT TUTARI dönülmektedir.</returns>
        IDataResult<CalcCreditListDto> CalcDecreasingCredit(double amount, int expiry, double interest, int DecreaseStartingInstallmentNumber,
            int DecreaseFrequency, int? DecreaseAmount, int? DecreasePercentage);
    }
}