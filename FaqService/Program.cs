using System.Reflection;
using FaqDataAccess;
using FaqService.Configurations;
using MediatR;
using Hellang.Middleware.ProblemDetails;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSettings(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.ConfigureDatabase();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddCustomProblemDetails();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<FaqDbContext>()
    .ForwardToPrometheus();

var app = builder.Build();

app.UseHttpMetrics();
app.MigrateDatabase();
app.UseRouting();
app.UseProblemDetails();
app.UseSwagger();
app.UseSwaggerUI();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapMetrics();
    endpoints.MapHealthChecks("/health");
});

Log.Logger.Information("Start in {Timezone}", TimeZoneInfo.Local.Id);

app.Run();