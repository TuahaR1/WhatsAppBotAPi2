using WhatsAppBotAPi.Services.Configurations;
using WhatsAppBotAPi.Services.Extensions;
using WhatsAppBotAPi.Services.Interfaces;
using WhatsAppBotAPi.Services.WhatsAppBusinessManager; // ✅ Needed for AddWhatsAppBotAPiService

var builder = WebApplication.CreateBuilder(args);
// You can configure logging providers if needed
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Load WhatsApp config from appsettings.json
var whatsAppConfig = builder.Configuration
    .GetSection("WhatsAppConfiguration")
    .Get<WhatsAppConfig>();

// Register WhatsApp Cloud API service
builder.Services.AddWhatsAppBotAPiService(whatsAppConfig);

builder.Services.AddScoped<IWhatsAppBussinesManager, WhatsAppBusinessManager>();
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
