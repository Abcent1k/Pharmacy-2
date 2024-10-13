namespace Pharmacy_2.Interfaces
{
	internal interface IGetProductService
	{
		public Task<IEnumerable<IProduct>> GetProductsAsync();
	}
}
