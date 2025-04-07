using FCamara.CommissionCalculator.Dto;
using FCamara.CommissionCalculator.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FCamara.CommissionCalculator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommisionController : ControllerBase
    {
        private readonly ILogger<CommisionController> _logger;
        private readonly ICommissionCalculator _commissionCalculator;
        public CommisionController(ICommissionCalculator commissionCalculator, ILogger<CommisionController> logger)
        {
            _commissionCalculator = commissionCalculator;
            _logger = logger;
        }


        [ProducesResponseType(typeof(CommissionCalculationResponseDto), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Calculate([FromBody] CommissionCalculationRequestDto calculationRequest)
        {
            var result = await _commissionCalculator.Calculate(calculationRequest.LocalSalesCount, calculationRequest.ForeignSalesCount, calculationRequest.AverageSaleAmount);
            if (result is null)
            {
                _logger.LogError("Invalid.Please Enter valid Inputs");
                return BadRequest("Invalid Input");
            }
            _logger.LogInformation($"FCamara Commission is {result.FCamaraCommissionAmount} & Competitor Commission is {result.CompetitorCommissionAmount}");
            return Ok(result);
        }
    }
}
