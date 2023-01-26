
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddSingleton<MessageService.TimerHub>();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:17005", "http://localhost:44424", "https://localhost:44424", "https://localhost:44324");
    }));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MessageService.TimerHub>("/timerHub", options =>
    {
        options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
    });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");



app.MapFallbackToFile("index.html");







app.Run();
