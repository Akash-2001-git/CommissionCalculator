namespace FCamara.CommissionCalculator.Dto
{
    public class CommissionCalculationRequestDto
    {
        public int LocalSalesCount { get; set; }
        public int ForeignSalesCount { get; set; }
        public decimal AverageSaleAmount { get; set; }
    }
}
