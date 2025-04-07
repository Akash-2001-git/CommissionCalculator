using FCamara.CommissionCalculator.Dto;

namespace FCamara.CommissionCalculator.Interface
{
    public interface ICommissionCalculator
    {
        public Task<CommissionCalculationResponseDto> Calculate(int localSales, int foreignSales, decimal averageAmount);
    }
}
