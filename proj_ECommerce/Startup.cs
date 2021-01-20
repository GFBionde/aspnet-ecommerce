using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace proj_ECommerce
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
            #region Serviço de Cookie Authorization

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            
            }).AddCookie(opt => {
                opt.Cookie.Name = "CookieAuth";
                opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Login/Index");
                opt.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login/Index");
                opt.ExpireTimeSpan = TimeSpan.FromDays(7);
            });

            
            services.AddAuthorization(opt => {
                opt.AddPolicy("CookieAuth", new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                   .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                   .RequireAuthenticatedUser().Build());
            });
            # endregion
            

            // Razor
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Default/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Default}/{action=Index}/{id?}");
            });
        }
    }
}
