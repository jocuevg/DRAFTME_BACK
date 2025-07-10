using DRAFTME_API.Middlewares;
using DRAFTME_BUSINESS.Extensions;
using DRAFTME_INFRA.Extensions;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


var config = builder.Configuration;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins, builder =>
{
    builder.AllowAnyMethod()
           .SetIsOriginAllowed(origin => true) // allow any origin 
           .AllowAnyHeader()
           .AllowCredentials();
}));

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10MB de límite
});

builder.Services.AddCustomDraftMeDBContext(config);
builder.Services.ConfigureInfraestructure();
builder.Services.AddBusinessLayer();

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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
