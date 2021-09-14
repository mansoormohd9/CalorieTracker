using CalorieTracker.Services;
using CalorieTracker.Services.Interfaces;
using CalorieTrackerApi.Data;
using CalorieTrackerApi.Mappings;
using CalorieTrackerApi.Repositories;
using CalorieTrackerApi.Repositories.Interfaces;
using CalorieTrackerApi.Services;
using CalorieTrackerApi.Services.Interfaces;
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

namespace CalorieTracker
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
            services.AddControllersWithViews();

            //Session Support
            services.AddDistributedMemoryCache();

            services.AddSession();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContextFactory<CalorieTrackerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Services from api
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFoodEntryService, FoodEntryService>();
            services.AddScoped<ITokenService, TokenService>();

            //Repos
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IFoodEntryRepo, FoodEntryRepo>();
            services.AddScoped<ITokenRepo, TokenRepo>();

            services.AddAutoMapper(typeof(MappingProfile));

            //Services
            services.AddScoped<IUserViewService, UserViewService>();
            services.AddScoped<IFoodEntryViewService, FoodEntryViewService>();
            services.AddScoped<ITokenViewService, TokenViewService>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Create}/{id?}");
            });
        }
    }
}
