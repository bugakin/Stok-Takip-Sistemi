using _2_StockControl_DataAccessLayer.Context;
using _2_StockControl_DataAccessLayer.Repositories.Abstract;
using _2_StockControl_DataAccessLayer.Repositories.Concrete;
using _3_StockControl_ServiceLayer.Services.Abstract;
using _3_StockControl_ServiceLayer.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Data Source=LAPTOP-3217KO0F\\SQLEXPRESS;Initial Catalog=AppStockControl;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"));

builder.Services.AddControllers().AddNewtonsoftJson(opt=>opt.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
