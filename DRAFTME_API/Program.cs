using DRAFTME_INFRA.Extensions;

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

builder.Services.AddCustomDraftMeDBContext(config);
builder.Services.ConfigureInfraestructure();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
