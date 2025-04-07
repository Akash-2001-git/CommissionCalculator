
namespace FCamara.CommissionCalculator.Models
{
    public class CommissionCalculation
    {
        public int LocalSalesCount { get; }
        public int ForeignSalesCount { get; }
        public decimal AverageSaleAmount { get; }

        private CommissionCalculation(int localSales, int foreignSales, decimal averageAmount)
        {
            LocalSalesCount = localSales;
            ForeignSalesCount = foreignSales;
            AverageSaleAmount = averageAmount;
        }

        public static CommissionCalculation Create(int localSales, int foreignSales, decimal averageAmount)
        {
            if (localSales < 0)
                throw new ArgumentOutOfRangeException(nameof(localSales), "Local sales count must be greater than or equal to 0.");

            if (foreignSales < 0)
                throw new ArgumentOutOfRangeException(nameof(foreignSales), "Foreign sales count must be greater than or equal to 0.");

            if (averageAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(averageAmount), "Average sale amount must be greater than 0.");

            return new CommissionCalculation(localSales, foreignSales, averageAmount);
        }
    }
}
