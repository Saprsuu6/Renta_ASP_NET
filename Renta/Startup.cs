using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Renta.Services.AutoMapper;
using RentaAPI.Models.Client;
using RentaAPI.Models.Contact;
using RentaAPI.Models.Good;
using RentaAPI.Services.Converters;

namespace Renta
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Mapper<Payment, DataBaseContext.Models.Client.Payment>>();
            services.AddSingleton<Mapper<Phone, DataBaseContext.Models.Contact.Phone>>();
            services.AddSingleton<Mapper<Apartment, DataBaseContext.Models.Good.Apartment>>();

            services.AddSingleton<Clients>();
            services.AddSingleton<Contacts>();
            services.AddSingleton<Orders>();

            //services.AddControllers();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Goods}/{action=Get}/{id?}");
            });
        }
    }
}
