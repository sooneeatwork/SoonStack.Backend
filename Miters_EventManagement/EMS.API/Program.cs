using EMS.Shared.DependencyInjection;
using EMS.Repository.TicketData;
using EMS.UseCases.TicketMgmt.Application.TicketModule.RepositoryInterfaces;
using EMS.UseCases.TicketMgmt.Application.TicketModule.DependencyInjection;
using EMS.Core.TicketMgmt.Dependency;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Configure connection string (assuming using IConfiguration)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception();
builder.Services.AddSingleton(connectionString); // or add it in a way that your services expect
builder.Services.AddSharedUseCases(builder.Configuration);

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketPurchaseHistoryRepository, TicketPurchaseHistoryRepository>();

// Add dependency injection for Ticket module
builder.Services.AddTicketDomain();
builder.Services.AddTicketModule();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var assemblies = AppDomain.CurrentDomain.GetAssemblies();
builder.Services.AddAutoMapper(assemblies);
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
