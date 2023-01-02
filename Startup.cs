using FastMechanical.Data;
using FastMechanical.Helper;
using FastMechanical.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMechanical {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();

            services.AddDbContext<BancoContext>(options => options.UseMySql(Configuration.GetConnectionString("BancoContext"), builder =>
               builder.MigrationsAssembly("FastMechanical")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<SeedingService>();
            services.AddScoped<IVeiculoServices, VeiculoServices>();
            services.AddScoped<IPessoaServices, PessoaServices>();
            services.AddScoped<IServicosServices, ServicosServices>();
            services.AddScoped<IAlmoxarifadoServices, AlmoxarifadoServices>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ISessionUser, Session>();

            services.AddSession(o => {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                seedingService.Seed();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                seedingService.Seed();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
