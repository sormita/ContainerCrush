using LoginFunctionality.Models;
using LoginFunctionality.Utility;
using LoginFunctionality.Utility.UtilityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace LoginFunctionality
{
    public partial class Startup
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
            services.Configure<SettingsModel>(Configuration.GetSection("ProjectSettings"));
            services.AddLogging();

            //services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedPhoneNumber = false)
            //    .AddDefaultTokenProviders();

            //services.AddIdentity<User, IdentityRole>(cfg =>
            //{
            //    cfg.User.RequireUniqueEmail = true;
            //});

            //services.AddAuthentication(cfg =>
            //{
            //    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}
            //    ).AddJwtBearer(cfg =>
            //{
            //    cfg.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = Configuration.GetSection("Issuer").ToString(),
            //        ValidateAudience = true,
            //        ValidAudience = Configuration.GetSection("Audience").ToString(),
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("SecretKey").ToString())),

            //    };
            //});

            services.AddScoped<IApiClient, ApiClient>();

            //services.AddScoped<IUserClaimsPrincipalFactory<User>, AppUserClaimsPrincipalFactory>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}");
            });
        }
    }
}
