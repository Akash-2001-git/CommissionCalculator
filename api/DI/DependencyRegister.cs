using FCamara.CommissionCalculator.Interface;
using FCamara.CommissionCalculator.Services;

namespace FCamara.CommissionCalculator.DI
{
    public static class DependencyRegister
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICommissionCalculator, CommissionCalculatorService>();
        }
    }
}
