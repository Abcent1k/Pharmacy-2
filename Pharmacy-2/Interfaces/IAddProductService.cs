namespace Pharmacy_2.Interfaces
{
	internal interface IAddProductService
	{
		public Task AddProductAsync(IProduct product);
	}
}
