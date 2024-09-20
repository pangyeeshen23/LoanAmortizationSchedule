using LoanApi.Handler;

namespace LoanApi
{
    public static class ProgramDI
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services) {
            services.AddScoped<ILoanHandler, LoanHandler>();
            return services;
        }
    }
}
