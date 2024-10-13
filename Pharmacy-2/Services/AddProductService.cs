using Pharmacy_2.Classes.Products;
using Pharmacy_2.Data;
using Pharmacy_2.Interfaces;

namespace Pharmacy_2.Services
{
	internal class AddProductService : IAddProductService
	{
		private readonly PharmacyContext _pharmacyContext;
		public AddProductService(PharmacyContext pharmacyContext)
		{
			_pharmacyContext = pharmacyContext;
		}

		public async Task AddProductAsync(IProduct product) {
			var existingProduct = await _pharmacyContext.Products.FindAsync(product.UPC);
			if (existingProduct != null)
			{
				throw new InvalidOperationException($"Product with UPC {product.UPC} already exists.");
			}

			_pharmacyContext.Products.Add((Product)product);
			await _pharmacyContext.SaveChangesAsync();
		}
	}
}
