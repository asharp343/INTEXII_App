using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using INTEXII_App.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using INTEXII_App.Models;
using INTEXII_App.Areas.Identity.Data;
using INTEXII_App.Models.AdminModel;

namespace INTEXII_App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllersWithViews();

            services.AddDbContext<BYU_ExcavationContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStringsDB"));
            });

            //trying to figure out login rn
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

                

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute("filter",
                //    "Burial/Details/{burialid}",
                //    new { controller = "Burial", action = "Index" });

                //endpoints.MapControllerRoute("filternpnopage",
                //    "Burial/Index/{filters}",
                //    new { controller = "Burial", action = "Filter", page = 1 });

                //endpoints.MapControllerRoute("filternpnopage",
                //    "Burial/Index/{page}",
                //    new { controller = "Burial", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            
            });
        }
    }
}
