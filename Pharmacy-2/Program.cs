using Microsoft.EntityFrameworkCore;
using Pharmacy_2.Data;
using Pharmacy_2.Extensions;
using Pharmacy_2.Interfaces;
using Pharmacy_2.Classes.Products;
using Pharmacy_2.Middleware;
using Pharmacy_2.Services;


var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PharmacyContext>(options => options.UseSqlServer(connection));

builder.Services.AddPharmacyServices();

var app = builder.Build();

app.UseMiddleware<ProductListMiddleware>();
app.UseMiddleware<AddProductMiddleware>();

app.Run(async (context) => context.Response.Redirect("/products"));

app.Run();