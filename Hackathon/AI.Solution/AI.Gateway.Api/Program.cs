// AI.Gateway.Api/Program.cs
using AI.Features.Analysis;
using AI.Infrastructure.Configuration;
using AI.Infrastructure.Services;
using AI.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dependencies
builder.Services.AddScoped<IOpenAIConfigurationProvider, AzureOpenAIConfigurationProvider>();
builder.Services.AddScoped<IPromptHandler, DefaultPromptHandler>();
builder.Services.AddScoped<ILLMService, AzureOpenAIService>();
builder.Services.AddScoped<IAnalyzeDocumentHandler, AnalyzeDocumentHandler>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();