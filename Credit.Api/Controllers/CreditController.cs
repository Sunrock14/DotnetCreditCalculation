using Credit.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Credit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : BaseController
    {
        private readonly IEqualCreditService _equalCreditService;
        private readonly IInterimPaymentCreditService _interimPaymentCreditService;
        private readonly IBallonCreditService _ballonCreditService;
        private readonly IGrowingCreditService _growingCreditService;
        private readonly IDecreasingCreditService _decreasingCreditService;
        private readonly ILogger<CreditController> _logger;

        public CreditController(ILogger<CreditController> logger, IEqualCreditService equalCreditService, IInterimPaymentCreditService interimPaymentCreditService,
            IBallonCreditService ballonCreditService, IDecreasingCreditService decreasingCreditService, IGrowingCreditService growingCreditService)
        {
            _logger = logger;
            _equalCreditService = equalCreditService;
            _interimPaymentCreditService = interimPaymentCreditService;
            _ballonCreditService = ballonCreditService;
            _decreasingCreditService = decreasingCreditService;
            _growingCreditService = growingCreditService;
        }

        /// <summary>
        /// Eşit Taksitli Kredi Hesaplama
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="expiry"></param>
        /// <param name="interest"></param>
        /// <returns></returns>
        /// <remarks>
        /// Örnek:
        ///     double amount : 100000,
        ///     uint amount : 12,
        ///     double interest : 0.99
        /// </remarks>

        [HttpGet("[action]")]
        public IActionResult GetEqualCredit(double amount, uint expiry, double interest)
        {
            var result = _equalCreditService.CalcEqualCredit(amount, expiry, interest);
            return CustomResponse(result);
        }
        /// <summary>
        /// Ara Ödemeli Kredi
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="expiry"></param>
        /// <param name="interest"></param>
        /// <param name="FirstPaymentNo"></param>
        /// <param name="InterimPaymentFrequency"></param>
        /// <param name="InterimPaymentAmount"></param>
        /// <param name="IsAddInterimAmount"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetInterimCredit(double amount, uint expiry, double interest,
            int FirstPaymentNo, int InterimPaymentFrequency, int InterimPaymentAmount, bool IsAddInterimAmount)
        {
            var result = _interimPaymentCreditService.GetCalcInterimCredit(amount, expiry, interest,
            FirstPaymentNo, InterimPaymentFrequency, InterimPaymentAmount, IsAddInterimAmount);
            return CustomResponse(result);
        }
        /// <summary>
        /// Balon Ödemeli Kredi Hesaplama
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="expiry"></param>
        /// <param name="interest"></param>
        /// <param name="InstallmentAmount"></param>
        /// <returns></returns>
        /// <remarks>
        /// Örnek:
        ///     double amount : 100000,
        ///     uint amount : 12,
        ///     double interest : 0.99
        /// </remarks>      
        [HttpGet("[action]")]
        public IActionResult GetBallonCredit(double amount, uint expiry, double interest, int InstallmentAmount)
        {
            var result = _ballonCreditService.GetCalcBallonCredit(amount, expiry, interest, InstallmentAmount);
            return CustomResponse(result);

        }
        /// <summary>
        /// Artan Taksitli Kredi Modeli
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
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetGrowingCredit(string creditModel, double amount, uint expiry, double interest, string paymentFrequency, int IncreaseStartInstallmentNumber,
            int IncreaseFrequency, int IncreaseAmount, int PercentageOfIncrease)
        {
            var result = _growingCreditService.GetGrowingCredit(creditModel, amount, expiry, interest, paymentFrequency, IncreaseStartInstallmentNumber,
            IncreaseFrequency, IncreaseAmount, PercentageOfIncrease);
            return CustomResponse(result);
        }
        /// <summary>
        /// Azalan Taksitli Kredi Modeli
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="expiry"></param>
        /// <param name="interest"></param>
        /// <param name="DecreaseStartingInstallmentNumber"></param>
        /// <param name="DecreaseFrequency"></param>
        /// <param name="DecreaseAmount"></param>
        /// <param name="DecreasePercentage"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetDescreasingCredit(double amount, int expiry, double interest, int DecreaseStartingInstallmentNumber,
            int DecreaseFrequency, int DecreaseAmount, int DecreasePercentage) 
        {
            var result = _decreasingCreditService.CalcDecreasingCredit(amount, expiry, interest, DecreaseStartingInstallmentNumber,
            DecreaseFrequency, DecreaseAmount, DecreasePercentage);
            return CustomResponse(result);
        }
    }
}
