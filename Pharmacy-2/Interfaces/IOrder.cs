namespace Pharmacy_2.Interfaces
{
	internal interface IOrder
	{
		decimal? TotalPrice { get; }
		DateTime? OrderDate { get; }
		void PlaceOrder();
	}
}
