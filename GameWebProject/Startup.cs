using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameWebProject.Models;
using GameWebProject.Models.SaleModel;
using GameWebProject.Models.UserModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameWebProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private IConfiguration _iconfig;
        public Startup(IConfiguration iconfig)
        {
            _iconfig = iconfig;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //if any Controller requested the IItemRepository service then create an instance of the MockItemRepository.
            // A singlton service is created when it is first requested. the same instance then will be used by other comming request.
            //services.AddSingleton<IItemRepository, MockItemRepository>();
            services.AddDbContextPool<AppDbContext>(opions => opions.UseSqlServer(_iconfig.GetConnectionString("ProjectDbConnection")));
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<IItemRepository, SqlItemRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("Error/{0}");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
