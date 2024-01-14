using CustomerSlices.DependencyManagement;
using DapperPersistance.DepedencyManagement;
using Infrastructure.Dependency;
using OrderSlices.DependencyInjection;
using ProductMgmtSlices.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // <-- Add this line
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDapperDependency(builder.Configuration);
builder.Services.AddInfraDependency();

builder.Services.AddOrderDependency();
builder.Services.AddCustomerDependency();
builder.Services.AddProductDependency();

// Integrate Dapper Persistence services
//DapperPersistence.DependencyManagement.AddPersistenceServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
