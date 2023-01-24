using Credit.Core.Utilities.Results.Abstract;
using Credit.Entities.Dtos;
using Credit.Services.Abstract;

namespace Credit.Services.Concrete
{
    public class GrowingCreditManager : IGrowingCreditService
    {

        public IDataResult<CalcCreditListDto> GetGrowingCredit(string creditModel, double amount, uint expiry, double interest, string paymentFrequency,
            int IncreaseStartInstallmentNumber, int IncreaseFrequency, int IncreaseAmount, int PercentageOfIncrease)
        {
            throw new NotImplementedException();
        }
    }
}
