using Learn_AspNetMVC.ExtendMethods;
using Learn_AspNetMVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_AspNetMVC
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
			services.Configure<RazorViewEngineOptions>(options =>
			{
				// {0} Ten Action
				// {1} Ten Controller
				// {2} Ten Area
				options.ViewLocationFormats.Add("/MyView/{1}/{0}"+RazorViewEngine.ViewExtension );
			});
			services.AddSingleton(typeof(ProductService), typeof(ProductService));
			services.AddSingleton<PlanetService>();
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
			app.AddStatusCodePage(); //Tuy bien khi co loi tu 400-599

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapControllerRoute(
				//	name: "default",
				//	pattern: "{controller=Home}/{action=Index}/{id?}");
				//endpoints.MapControllers();
				//endpoints.MapControllerRoute();
				//endpoints.MapDefaultControllerRoute();
				//endpoints.MapAreaControllerRoute();

				endpoints.MapControllerRoute(
					name: "first",
					//ksdksd/3
					//Home/3
					pattern: "{url:regex(^((xemsanpham)||(viewproduct))$)}/{id:range(2,4)}",
					defaults: new
					{
						controller = "First",
						action = "ViewProduct"
					}
					//constraints: new
					//{
					//	//url=new RegexRouteConstraint(@"^((xemsanpham)||(viewproduct))$"),
					//	//id=new RangeRouteConstraint(2,4)
					//}
					);
				endpoints.MapAreaControllerRoute(
					name: "firstroute",
					pattern: "{controller}/{action=Index}/{id?}",
					areaName: "ProductManage"
					);
				endpoints.MapControllerRoute(
					name: "firstroute",
					pattern: "{controller=Home}/{action=Index}/{id?}"
					//defaut: controller, action, area
					//defaults: new
					//{
					//	//controller = "First",
					//	//action = "ViewProduct",
					//	//id = 3
					//}
				);
            });
		}
	}
}
