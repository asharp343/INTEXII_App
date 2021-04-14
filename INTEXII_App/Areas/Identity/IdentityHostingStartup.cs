﻿using System;
using INTEXII_App.Areas.Identity.Data;
using INTEXII_App.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(INTEXII_App.Areas.Identity.IdentityHostingStartup))]
namespace INTEXII_App.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthDbContextConnection")));


                services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequiredLength = 10; //require at least 10 characters
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                }) 
                    .AddRoles<IdentityRole>()  
                    .AddEntityFrameworkStores<AuthDbContext>();

                services.ConfigureApplicationCookie(options =>
                {
                    //options.LoginPath = new PathString("/[your-path]");
                    options.AccessDeniedPath = new PathString("/Home/PermissionDenied");
                    //options.LogoutPath = new PathString("/[your-path]");
                });

            });

        }
    }
}