using Microsoft.Extensions.DependencyInjection;
using Pharmacy_2.Interfaces;
using Pharmacy_2.Services;

namespace Pharmacy_2.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPharmacyServices(this IServiceCollection services)
		{
			// Реєстрація сервісів
			services.AddScoped<IGetProductService, GetProductService>();
			services.AddScoped<IAddProductService, AddProductService>();

			return services;
		}
	}
}