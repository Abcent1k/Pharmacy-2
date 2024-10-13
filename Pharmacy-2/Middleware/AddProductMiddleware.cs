using System.Text;
using Pharmacy_2.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy_2.Classes.Products;

namespace Pharmacy_2.Middleware
{
	public class AddProductMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IServiceScopeFactory _scopeFactory;

		public AddProductMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
		{
			_next = next;
			_scopeFactory = scopeFactory;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Path == "/AddProduct")
			{
				if (context.Request.Method == HttpMethods.Get)
				{
					await context.Response.WriteAsync(GetFormHtml());
				}
				else if (context.Request.Method == HttpMethods.Post)
				{
					using (var scope = _scopeFactory.CreateScope())
					{
						var addProductService = scope.ServiceProvider.GetRequiredService<IAddProductService>();

						context.Request.EnableBuffering();
						var formData = await context.Request.ReadFormAsync();
						var productType = formData["ProductType"];

						IProduct product = null;

						switch (productType)
						{
							case "Consumables":
								product = new Consumables(
									uint.Parse(formData["UPC"]),
									formData["Name"],
									decimal.Parse(formData["Price"]),
									uint.Parse(formData["EDRPOU"]),
									DateTime.Parse(formData["ExpirationDate"]),
									(ConsumableType)Enum.Parse(typeof(ConsumableType), formData["ConsumableType"])
								);
								break;

							case "Devices":
								product = new Devices(
									uint.Parse(formData["UPC"]),
									formData["Name"],
									decimal.Parse(formData["Price"]),
									uint.Parse(formData["EDRPOU"]),
									(DeviceType)Enum.Parse(typeof(DeviceType), formData["DeviceType"])
								);
								break;

							case "Drugs":
								product = new Drugs(
									uint.Parse(formData["UPC"]),
									formData["Name"],
									decimal.Parse(formData["Price"]),
									uint.Parse(formData["EDRPOU"]),
									DateTime.Parse(formData["ExpirationDate"]),
									(DrugType)Enum.Parse(typeof(DrugType), formData["DrugType"]),
									bool.Parse(formData["NeedRecipe"])
								);
								break;

							default:
								context.Response.StatusCode = StatusCodes.Status400BadRequest;
								await context.Response.WriteAsync("Invalid product type.");
								return;
						}

						await addProductService.AddProductAsync(product);

						await context.Response.WriteAsync("Product added successfully.");
					}
				}
			}
			else
			{
				await _next(context);
			}
		}

		private string GetFormHtml()
		{
			var sb = new StringBuilder();
			sb.AppendLine("<html><body>");
			sb.AppendLine("<form method='post'>");
			sb.AppendLine("UPC: <input type='text' name='UPC' required><br>");
			sb.AppendLine("Name: <input type='text' name='Name' required><br>");
			sb.AppendLine("Price: <input type='text' name='Price' required><br>");
			sb.AppendLine("EDRPOU: <input type='text' name='EDRPOU' required><br>");
			sb.AppendLine("Product Type: <select name='ProductType' required>");
			sb.AppendLine("<option value='Consumables'>Consumables</option>");
			sb.AppendLine("<option value='Devices'>Devices</option>");
			sb.AppendLine("<option value='Drugs'>Drugs</option>");
			sb.AppendLine("</select><br>");
			sb.AppendLine("Consumable Type: <select name='ConsumableType'>");
			sb.AppendLine("<option value='Patch'>Patch</option>");
			sb.AppendLine("<option value='Syringe'>Syringe</option>");
			sb.AppendLine("<option value='Needle'>Needle</option>");
			sb.AppendLine("<option value='Gauze'>Gauze</option>");
			sb.AppendLine("</select><br>");
			sb.AppendLine("Device Type: <select name='DeviceType'>");
			sb.AppendLine("<option value='Inhaler'>Inhaler</option>");
			sb.AppendLine("<option value='Pulse_oximetr'>Pulse Oximetr</option>");
			sb.AppendLine("<option value='Blood_pressure_monitor'>Blood Pressure Monitor</option>");
			sb.AppendLine("</select><br>");
			sb.AppendLine("Drug Type: <select name='DrugType'>");
			sb.AppendLine("<option value='Pills'>Pills</option>");
			sb.AppendLine("<option value='Spray'>Spray</option>");
			sb.AppendLine("<option value='Drops'>Drops</option>");
			sb.AppendLine("<option value='Syrop'>Syrop</option>");
			sb.AppendLine("<option value='Inhalation_solution'>Inhalation Solution</option>");
			sb.AppendLine("<option value='Injection_solution'>Injection Solution</option>");
			sb.AppendLine("</select><br>");
			sb.AppendLine("Expiration Date: <input type='date' name='ExpirationDate'><br>");
			sb.AppendLine("Need a recipe: <input type='checkbox' name='NeedRecipe' value='true'><br>");
			sb.AppendLine("<input type='submit' value='Add Product'>");
			sb.AppendLine("</form>");
			sb.AppendLine("</body></html>");
			return sb.ToString();
		}
	}
}
