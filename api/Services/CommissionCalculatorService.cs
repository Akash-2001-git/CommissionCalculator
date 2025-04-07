using FCamara.CommissionCalculator.Dto;
using FCamara.CommissionCalculator.Interface;
using FCamara.CommissionCalculator.Models;

namespace FCamara.CommissionCalculator.Services
{
    public class CommissionCalculatorService : ICommissionCalculator
    {
        private readonly IConfiguration _configuration;
        // making discounts configurable through appsettings.json
        private readonly decimal _fCamaraLocalRate;
        private readonly decimal _fCamaraForeignRate;
        private readonly decimal _competitorLocalRate;
        private readonly decimal _competitorForeignRate;
        private readonly ILogger<CommissionCalculatorService> _logger;

        public CommissionCalculatorService(IConfiguration configuration, ILogger<CommissionCalculatorService> logger)
        {
            _configuration = configuration;
            _fCamaraLocalRate = ConvertToPercent(_configuration["Discount:FCamaraLocalRate"]);
            _fCamaraForeignRate = ConvertToPercent(_configuration["Discount:FCamaraForeignRate"]);
            _competitorLocalRate = ConvertToPercent(_configuration["Discount:CompetitorLocalRate"]);
            _competitorForeignRate = ConvertToPercent(_configuration["Discount:CompetitorForeignRate"]);
            _logger = logger;
        }

        private static decimal ConvertToPercent(string? v)
        {
            if (decimal.TryParse(v, out var value))
                return value / 100m;
            return default;
        }

        public async Task<CommissionCalculationResponseDto> Calculate(int localSales, int foreignSales, decimal averageAmount)
        {
            try
            {
                var commission = CommissionCalculation.Create(localSales, foreignSales, averageAmount);

                return await Task.FromResult(new CommissionCalculationResponseDto
                {
                    FCamaraCommissionAmount = GetFCamaraCommission(commission.LocalSalesCount, commission.AverageSaleAmount, commission.ForeignSalesCount),
                    CompetitorCommissionAmount = GetCompetitorCommission(commission.LocalSalesCount, commission.AverageSaleAmount, commission.ForeignSalesCount)
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred");
                //returning null to handle in controller
                return null!;
            }
        }

        private decimal GetFCamaraCommission(int localSales, decimal averageAmount, int foreignSales)
        {
            return (localSales * averageAmount * _fCamaraLocalRate)
                 + (foreignSales * averageAmount * _fCamaraForeignRate);
        }

        private decimal GetCompetitorCommission(int localSales, decimal averageAmount, int foreignSales)
        {
            return (localSales * averageAmount * _competitorLocalRate)
                 + (foreignSales * averageAmount * _competitorForeignRate);
        }
    }
}
