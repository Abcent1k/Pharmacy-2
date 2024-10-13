using System.Text;
using Pharmacy_2.Interfaces;

namespace Pharmacy_2.Middleware
{
	internal class ProductListMiddleware
	{
		private readonly RequestDelegate _next;

		public ProductListMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, IGetProductService getProductService)
		{
			if (context.Request.Path == "/products")
			{
				var products = await getProductService.GetProductsAsync();

				var stringBuilder = new StringBuilder();
				stringBuilder.Append("<html><head><meta charset='utf-8'><title>Product List</title></head><body>");
				stringBuilder.Append("<h1>Product List</h1>");
				stringBuilder.Append("<table border='1'>");
				stringBuilder.Append("<tr><th>UPC</th><th>Name</th><th>Price</th><th>EDRPOU</th></tr>");

				foreach (var product in products)
				{
					stringBuilder.Append($"<tr><td>{product.UPC}</td><td>{product.Name}</td><td>{product.Price}</td><td>{product.EDRPOU}</td></tr>");
				}

				stringBuilder.Append("</table>");
				stringBuilder.Append("<br/><a href='/AddProduct'>Add Product</a>");
				stringBuilder.Append("</body></html>");

				context.Response.ContentType = "text/html; charset=utf-8";
				await context.Response.WriteAsync(stringBuilder.ToString());
				return;
			}

			await _next(context);
		}
	}
}
