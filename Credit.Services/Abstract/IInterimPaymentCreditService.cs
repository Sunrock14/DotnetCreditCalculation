using Credit.Core.Utilities.Results.Abstract;
using Credit.Entities.Dtos;

namespace Credit.Services.Abstract
{
    public interface IInterimPaymentCreditService
    {
        /// <summary>
        /// Bu işlem eşit ödemeli krediye benzer farklı olarak kullanıcının belirlediği sıklıkta ve tutarda ekstra olarak ödeme yaptığı kredi modeldir.
        /// ÖR
        /// 4 ayda bir 1000 ₺ ara ödemeli bir kredi tipi olsun
        /// 1-15.03.20xx- 3558,65 ₺
        /// 2-15.04.20xx- 3558,65 ₺
        /// 3-15.05.20xx- 4558,65 ₺
        /// 4-15.06.20xx- 3558,65 ₺
        /// ...
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="expiry"></param>
        /// <param name="interest"></param>
        /// <param name="FirstPaymentNo"></param>
        /// <param name="InterimPaymentFrequency"></param>
        /// <param name="InterimPaymentAmount"></param>
        /// <param name="IsAddInterimAmount"></param>
        /// <returns name="CalcCreditListDto">İstek yapan kullanıcılara dönülen modeldir.Klasik olarak NO-TARİH-TAKSİT TUTARI dönülmektedir.</returns>
        IDataResult<CalcCreditListDto> GetCalcInterimCredit(double amount, int expiry, double interest,
            int FirstPaymentNo, int InterimPaymentFrequency, int InterimPaymentAmount, bool IsAddInterimAmount);
    }
}

