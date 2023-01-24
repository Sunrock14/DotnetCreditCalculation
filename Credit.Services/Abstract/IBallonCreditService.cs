using Credit.Core.Utilities.Results.Abstract;
using Credit.Entities.Dtos;

namespace Credit.Services.Abstract
{
    public interface IBallonCreditService
    {
        /// <summary>
        /// Kredi geri ödemelerinin nispeten düşük taksit tutarlarıyla yapılıp kalan borcun son taksitte kapatıldığı kredi modelidir.
        /// ÖR
        /// 1-15.03.20xx- 2000,00 ₺
        /// 2-15.04.20xx- 2000,00 ₺
        /// ...
        /// 24-15.03.20xx- 55451,97 ₺
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="expiry"></param>
        /// <param name="interest"></param>
        /// <param name="InstallmentAmount"></param>
        /// <returns name="CalcCreditListDto">İstek yapan kullanıcılara dönülen modeldir.Klasik olarak NO-TARİH-TAKSİT TUTARI dönülmektedir.</returns>
        IDataResult<CalcCreditListDto> GetCalcBallonCredit(double amount, uint expiry, double interest, int InstallmentAmount);

    }
}