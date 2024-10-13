using Microsoft.EntityFrameworkCore;
using Pharmacy_2.Data;
using Pharmacy_2.Interfaces;

namespace Pharmacy_2.Services
{
	internal class GetProductService : IGetProductService
	{
		private readonly PharmacyContext _pharmacyContext;
		public GetProductService(PharmacyContext pharmacyContext) {
			_pharmacyContext = pharmacyContext;
		}

		public async Task<IEnumerable<IProduct>> GetProductsAsync()
		{
			// Получаем все продукты из базы данных асинхронно
			return await _pharmacyContext.Products.ToListAsync();
		}
	}
}
