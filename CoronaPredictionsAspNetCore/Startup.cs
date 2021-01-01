using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaPredictionsAspNetCore.Areas.Identity.Data;
using CoronaPredictionsAspNetCore.DataAccessLayer;
using CoronaPredictionsAspNetCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;

namespace CoronaPredictionsAspNetCore
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
            //to evala sto delete role. Gia na mporei na diagrapsei rolo prepei na exei ta 2 claims(Delete & Create Role)
            services.AddAuthorization(options=> 
                                      options.AddPolicy("DeleteRolePolicy", policy=>
                                                                            policy.RequireClaim("Delete Role","true")
                                                                                  .RequireClaim("Create Role", "true")));
            services.AddControllersWithViews();
            services.AddScoped<IPredictionsRepo, PredictionsRepo>();
            services.AddScoped<IRealCasesRepo, RealCasesRepo>();
            services.AddScoped<IStandingsRepo, StandingsRepo>();
            services.AddScoped<ISystemPointsRepo, SystemPointsRepo>();
            services.AddScoped<IPlayersRepo, PlayersRepo>();
            services.AddDbContext<PredictCoronaCasesDBContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("CoronaPredictionsDBConnStr")));
            //Default token lifespan is 1 day
            //services.Configure<DataProtectionTokenProviderOptions>(o=>o.TokenLifespan=TimeSpan.FromDays(2));
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               app.UseHsts();
            }
           // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
