using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PierwszaAplikacjaMVC.Models;

namespace PierwszaAplikacjaMVC
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // rejestrujemy nasze repozytorium, chcemy w miejsce IResponseRepository otrzymywać ResponseRepository
            // staramy się unikać silnego typowania w aplikacji, ułatwi to nam później ewentualne rozszerzanie
            // lub przeprowadzanie testów:
            services.AddTransient<IResponsesRepository, ResponsesRepository>();

            // dodajemy obsługę wzorca model-widok-kontroler w aplikacji:
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // włączamy obsługę szczegółowej listy wyjątków
                app.UseStatusCodePages(); // włączamy obsługę kodów HTTP
            }

            app.UseStaticFiles(); // włączamy obsługę statycznych plików z katalogu /wwwroot

            // włączamy obsługę mechanizmów MVC oraz jako parametr podajemy 
            // delegat w postaci wyrażenia lambda (blok kodu) reprezentujący domyślne ścieżki w aplikacji:
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "impreza",
                    defaults: new { controller = "Home", action = "ResponseForm"}
                );

                routes.MapRoute(
                    name: null,
                    template: "impreza/lista",
                    defaults: new { controller = "Home", action = "ListResponses"}
                );
                
                // główna domyślna ścieżka:
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            // Tak możemy użyć też głównej domyślnej ścieżki:
            // app.UseMvcWithDefaultRoute();
        }
    }
}
