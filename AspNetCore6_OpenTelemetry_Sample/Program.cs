using OpenTelemetry.Logs;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Exporter;
using SomeApplicationProject;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<RocketService>();

builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(options =>
{
    options.AddOtlpExporter(configure =>
    {
        configure.Endpoint = new Uri("http://localhost:43170");
        configure.Protocol = OtlpExportProtocol.Grpc;
    });
});
builder.Services.AddOpenTelemetryTracing(options =>
{
    options.SetSampler(new AlwaysOnSampler())
           .AddHttpClientInstrumentation()
           .AddAspNetCoreInstrumentation()
           .AddOtlpExporter(configure =>
           {
               configure.Endpoint = new Uri("http://localhost:43170");
               configure.Protocol = OtlpExportProtocol.Grpc;
           })
           .AddConsoleExporter();

});

builder.Services.AddOpenTelemetryMetrics(options =>
{
    options.AddRuntimeInstrumentation()
           .AddHttpClientInstrumentation()
           .AddAspNetCoreInstrumentation()
           .AddOtlpExporter(configure =>
           {
               configure.Endpoint = new Uri("http://localhost:43170");
               configure.Protocol = OtlpExportProtocol.Grpc;
           })
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
