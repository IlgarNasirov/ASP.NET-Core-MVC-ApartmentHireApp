using ApartmentHireApp.Models;
using ApartmentHireApp.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddCors();
builder.Services.AddDbContext<ApartmentHireContext>(ServiceLifetime.Transient);
builder.Services.AddScoped<IBlockRepository, BlockRepository>();
builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
var app = builder.Build();
app.UseStaticFiles();
app.UseStatusCodePagesWithReExecute("/Error/Error404", "?code={0}");
app.UseExceptionHandler("/Error/Error500");
app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Contract}/{Action=Contracts}");
app.Run();