using System;
using CoronaPredictionsAspNetCore.Areas.Identity.Data;
using CoronaPredictionsAspNetCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

[assembly: HostingStartup(typeof(CoronaPredictionsAspNetCore.Areas.Identity.IdentityHostingStartup))]
namespace CoronaPredictionsAspNetCore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
       
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthDbContextConnection")));
                var mailKitOptions = context.Configuration.GetSection("EmailOptions").Get<MailKitOptions>();
                services.AddMailKit(conf=>conf.UseMailKit(mailKitOptions));
                services.AddDefaultIdentity<ApplicationUser>(options => {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                })
                .AddRoles<IdentityRole>() .AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();
              
            });
        }
    }
}