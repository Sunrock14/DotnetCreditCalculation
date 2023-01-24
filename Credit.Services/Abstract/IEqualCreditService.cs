using Credit.Core.Utilities.Results.Abstract;
using Credit.Entities.Dtos;

namespace Credit.Services.Abstract
{
    public interface IEqualCreditService
    {
        /// <summary>
        /// Bu işlem klasik olarak kredi hesaplaması yapar. Her taksit taksit eşit tutardadır.(Son taksit kalan küsüratlı kalıp eşit olmayabilir)
        /// ÖR
        /// 1-15.03.20xx- 3750,45 ₺
        /// 2-15.04.20xx- 3750,45 ₺
        /// ...
        /// 24-15.03.20xx- 3750,12 ₺
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="expiry"></param>
        /// <param name="interest"></param>
        /// <returns name="CalcCreditListDto">İstek yapan kullanıcılara dönülen modeldir.Klasik olarak NO-TARİH-TAKSİT TUTARI dönülmektedir.</returns>
        IDataResult<CalcCreditListDto> CalcEqualCredit(double amount, int expiry, double interest);
    }
}