using System.Text.Json.Serialization;
using Back_End_Dot_Net.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add database connection string
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
    // builder.Configuration.GetConnectionString("DefaultConnection")
    builder.Configuration.GetConnectionString("OnlineConnection")
));

// Add JSON serialization for using HTTP PATCH
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add logging info using Serilog
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Make enum data type represent as strings in SwaggerUI
builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {

        options.AddDefaultPolicy(
            policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });
}


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

//CORs
app.UseCors();

app.Run();
