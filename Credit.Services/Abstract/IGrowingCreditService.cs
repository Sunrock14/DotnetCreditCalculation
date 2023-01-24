using Credit.Core.Utilities.Results.Abstract;
using Credit.Entities.Dtos;

namespace Credit.Services.Abstract
{
    public interface IGrowingCreditService
    {
        /// <summary>
        /// Bu işlem her taksitin belli bir miktarda artarak hesaplanan kredi modelidir. Yüzdelik olarak veya belli bir miktarda artmakdır.
        /// ÖR
        /// 1-15.03.20xx- 3512,98 ₺
        /// 2-15.04.20xx- 3623,69 ₺
        /// 3-15.05.20xx- 3750,15 ₺
        /// ...
        /// </summary>
        /// <param name="creditModel"></param>
        /// <param name="amount"></param>
        /// <param name="expiry"></param>
        /// <param name="interest"></param>
        /// <param name="paymentFrequency"></param>
        /// <param name="IncreaseStartInstallmentNumber"></param>
        /// <param name="IncreaseFrequency"></param>
        /// <param name="IncreaseAmount"></param>
        /// <param name="PercentageOfIncrease"></param>
        /// <returns name="CalcCreditListDto">İstek yapan kullanıcılara dönülen modeldir.Klasik olarak NO-TARİH-TAKSİT TUTARI dönülmektedir.</returns>
        IDataResult<CalcCreditListDto> GetGrowingCredit(string creditModel, double amount, uint expiry, double interest, string paymentFrequency, int IncreaseStartInstallmentNumber,
            int IncreaseFrequency, int IncreaseAmount, int PercentageOfIncrease);
    }
}

