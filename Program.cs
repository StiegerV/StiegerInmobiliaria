using StiegerModels;

PropietarioModel a=new PropietarioModel("44359371","ricardo","malardo","266469420");
RepositorioPropietario repoP=new RepositorioPropietario();
/*repoP.Alta(a);
repoP.Baja(b);
PropietarioModel b= repoP.TraerId(4);
PropietarioModel c= repoP.TraerId(6);
c.Apellido="vizcay";
repoP.Modificacion(c);*/
var lista=repoP.TraerTodos();

foreach (var item in lista)
{
    Console.WriteLine(item.ToString());
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
