using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Multitenancy.Core.Extensions;
using Multitenancy.Web.Controllers;

namespace Multitenancy.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMultitenancy(builder => 
            builder.UseSqlServer("Server=localhost;Database=Multitenancy;Trusted_Connection=True;")).EnableEntityFrameworkCore();
            // services.AddMultitenancy<MyTenant, Guid>(builder =>
            //     builder.UseInMemoryDatabase("Multitenancy"));
            // builder.UseSqlServer("Server=localhost;Database=Multitenancy;Trusted_Connection=True;"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
