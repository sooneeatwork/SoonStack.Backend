using DocumentProcessingAPI.Infrastructure.Configuration;
using Features.DocumentProcessing.Application.Interfaces;
using Features.DocumentProcessing.Application.Services;
using DocumentProcessingAPI.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Document Processing API",
        Version = "v1",
        Description = "API for processing and analyzing PDF documents"
    });
});

// Validate configuration
var geminiApiKey = builder.Configuration["Gemini:ApiKey"];
if (string.IsNullOrEmpty(geminiApiKey))
{
    throw new InvalidOperationException("Gemini API key is not configured. Please add 'Gemini:ApiKey' to your configuration.");
}

// Configure AWS Services
builder.Services.AddAWSServices(builder.Configuration);

// Register Services
builder.Services.AddScoped<IPdfAnalysisService>(sp =>
    new GeminiPdfAnalysisService(
        geminiApiKey,
        sp.GetRequiredService<ILogger<GeminiPdfAnalysisService>>()
    ));

// Register Document Service
builder.Services.AddScoped<IDocumentService, DocumentService>();

// CORS Policy (if needed)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Add your frontend URL
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Document Processing API V1");
    });
}

// Create uploads directory if it doesn't exist
var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
Directory.CreateDirectory(uploadsPath);

app.UseHttpsRedirection();
app.UseAuthorization();

// Use CORS before routing
app.UseCors("AllowSpecificOrigins");

app.MapControllers();

app.Run();