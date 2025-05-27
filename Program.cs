using StiegerInmobiliaria.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using StiegerInmobiliaria.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options =>
    {
        options.LoginPath = "/Usuario/LoginView";
        options.LogoutPath = "/Usuario/Logout";
        options.AccessDeniedPath = "/Home/Restringido";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("administrador", policy => policy.RequireRole("administrador"));
});

//inyeccion de dependencias
builder.Services.AddScoped<IrepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<iRepositorioContrato, RepositorioContrato>();
builder.Services.AddScoped<IrepositorioInmueble, RepositorioInmueble>();
builder.Services.AddScoped<IrepositorioInquilino, RepositorioInquilino>();
builder.Services.AddScoped<IrepositorioPagos, RepositorioPago>();
builder.Services.AddScoped<IrepositorioPropietario, RepositorioPropietario>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//ver el flujo de peticiones
app.UseMiddleware<RequestLoggingMiddleware>();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
