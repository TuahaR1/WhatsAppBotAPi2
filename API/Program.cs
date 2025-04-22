using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WhatsAppBotAPi.Services.Configurations;
using WhatsAppBotAPi.Services.Extensions;
using WhatsAppBotAPi.Services.Interfaces;
using WhatsAppBotAPi.Services.WhatsAppBusinessManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container for Web API.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Configure WhatsApp Bot API service
builder.Services.Configure<WhatsAppConfig>(options =>
{
    builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration").Bind(options);
});

WhatsAppConfig whatsAppConfig = new WhatsAppConfig();
whatsAppConfig.WhatsAppBusinessPhoneNumberId = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["WhatsAppBusinessPhoneNumberId"];
whatsAppConfig.AppID = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["AppID"];
whatsAppConfig.WhatsAppBusinessId = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["WhatsAppBusinessId"];
whatsAppConfig.AccessToken = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["AccessToken"];
whatsAppConfig.AppName = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["AppName"];
whatsAppConfig.Version = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["ApiVersion"];
whatsAppConfig.BaseUrl = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["BaseUrl"];
whatsAppConfig.MyWhatsappNo = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["MyWhatsappNo"];
whatsAppConfig.MyAccessToken = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["MyAccessToken"];

builder.Services.AddWhatsAppBotAPiService(whatsAppConfig);
builder.Services.AddScoped<IWhatsAppBussinesManager, WhatsAppBusinessManager>();
// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WhatsApp Bot API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WhatsApp Bot API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();  // Must be called before UseAuthorization

app.UseAuthorization();

app.MapControllers(); // Map Web API controllers

app.Run();
