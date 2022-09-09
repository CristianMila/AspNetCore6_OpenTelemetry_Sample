using OpenTelemetry.Logs;
using OpenTelemetry.Trace;
using SomeApplicationProject;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<RocketService>();

builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(options =>
{
    options.AddConsoleExporter();
});
builder.Services.AddOpenTelemetryTracing(options =>
{
    options.SetSampler(new AlwaysOnSampler())
           .AddHttpClientInstrumentation()
           .AddAspNetCoreInstrumentation()
           .AddConsoleExporter();

});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
