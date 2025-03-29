using StiegerModels;

namespace StiegerInmobiliaria
{
    public class Startup
    {

        private readonly IConfiguration configuration;
        public Startup(IConfiguration config)
        {
            this.configuration = config;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            /*
            transient: crea una instancia por peticion del servicio
            scoped: "" solicitud http
            singleton:"" una sola vez y se utiliza mientras ande el serv
            */
            services.AddTransient<iRepositorio<PropietarioModel>, RepositorioPropietario>();
            services.AddTransient<iRepositorio<InquilinoModel>, RepositorioInquilino>();
            services.AddControllersWithViews();
        }

        //configurar los middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // error generico
                app.UseHsts(); // Fuerza el uso de HTTPS en producción
            }

            //archivos estaticos
            app.UseStaticFiles(); 
            //enrutamiento
            app.UseRouting(); 

        }
    }
}