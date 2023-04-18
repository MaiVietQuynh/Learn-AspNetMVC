using Learn_AspNetMVC.Data;
using Learn_AspNetMVC.ExtendMethods;
using Learn_AspNetMVC.Models;
using Learn_AspNetMVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
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
            services.AddOptions();
            var mailsetting = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsetting);

            services.AddSingleton<IEmailSender, SendMailService>();

            services.AddSingleton<IdentityErrorDescriber,AppIdentityErrorDescriber>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AppDbContext"));
            });
            services.AddControllersWithViews();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                // {0} Ten Action
                // {1} Ten Controller
                // {2} Ten Area
                options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);
            });
            services.AddSingleton(typeof(ProductService), typeof(ProductService));
            services.AddSingleton<PlanetService>();
            //Dang ky Identity
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 3 lần thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại

                options.SignIn.RequireConfirmedAccount = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login/";
                options.LoginPath = "/logout/";
                options.AccessDeniedPath = "/khongduoctruycap.html";
            });

            services.AddAuthentication()
                .AddGoogle(googleOption =>
                {
                    IConfigurationSection googleConfig = Configuration.GetSection("Authentication:Google");
                    googleOption.ClientId = googleConfig["ClientId"];
                    googleOption.ClientSecret = googleConfig["ClientSecret"];
                    googleOption.CallbackPath = "/dang-nhap-tu-google";
                    //CallbackPath mac dinh : https://localhost:5001/signin-google
                })
                .AddFacebook(facebookOptions =>
                {
                    IConfigurationSection facebookConfig = Configuration.GetSection("Authentication:Facebook");
                    facebookOptions.AppId = facebookConfig["AppId"];
                    facebookOptions.AppSecret = facebookConfig["AppSecret"];
                    facebookOptions.CallbackPath = "/dang-nhap-tu-facebook";

                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("ViewManageMenu", builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.RequireRole(RoleName.Administrator);
                });
            });
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
            app.UseAuthentication();
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
