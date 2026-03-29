using System.Collections.Concurrent;
using Lab4.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ICsvStorageService, CsvStorageService>();

var app = builder.Build();

var logFilePath = Path.Combine(app.Environment.ContentRootPath, "requests.log");
var logLocks = new ConcurrentDictionary<string, object>();

app.Use(async (context, next) =>
{
    await next();

    var remoteIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    var method = context.Request.Method;
    var path = context.Request.Path.HasValue ? context.Request.Path.Value : "/";
    var statusCode = context.Response.StatusCode;

    var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{remoteIp}\t{method}\t{path}\t{statusCode}";
    var fileLock = logLocks.GetOrAdd(logFilePath, _ => new object());

    lock (fileLock)
    {
        File.AppendAllLines(logFilePath, [line]);
    }
});

app.UseStatusCodePagesWithReExecute("/404");

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapControllers();

app.MapGet("/index.html", (HttpContext context) =>
{
    context.Response.Redirect("/", permanent: true);
    return Task.CompletedTask;
});

app.MapGet("/about.html", (HttpContext context) =>
{
    context.Response.Redirect("/about", permanent: true);
    return Task.CompletedTask;
});

app.MapGet("/contact.html", (HttpContext context) =>
{
    context.Response.Redirect("/contact", permanent: true);
    return Task.CompletedTask;
});

app.MapGet("/privacy.html", (HttpContext context) =>
{
    context.Response.Redirect("/privacy", permanent: true);
    return Task.CompletedTask;
});

app.MapGet("/tos.html", (HttpContext context) =>
{
    context.Response.Redirect("/tos", permanent: true);
    return Task.CompletedTask;
});

app.Run();
