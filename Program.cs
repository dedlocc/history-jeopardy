using HistoryJeopardy.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddSession();
services.AddControllersWithViews();
services.AddRouting(options => options.LowercaseUrls = true);

services.AddSingleton<PlayerService>();
services.AddSingleton<GameService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseSession();

app.UseWebSockets();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");

app.Run();