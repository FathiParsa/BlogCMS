using DataAccessLayer.EF.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Wordpress
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BloggingDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddAuthentication("mft")
                .AddCookie("mft", options =>
                {
                    options.AccessDeniedPath = new PathString("/Account/");
                    options.Cookie = new CookieBuilder
                    {
                        // Domain = localhost 
                        HttpOnly = true,
                        Name = "mft.Cookie",
                        Path = "/",
                        SameSite = SameSiteMode.Strict,
                        SecurePolicy = CookieSecurePolicy.Always

                    };
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnSignedIn = context =>
                        {
                            Console.WriteLine("{0} - {1} : {2}" , DateTime.Now , "OnSignedIn" , context.Principal.Identity.Name);
                            return Task.CompletedTask;
                        },
                        OnValidatePrincipal = context =>
                        {
                            try
                            {
                                if (context.Principal.Identity.IsAuthenticated)
                                {
                                   return Task.CompletedTask;
                                }
                                return Task.CompletedTask;
                            }
                            catch (Exception e)
                            {
                                context.RejectPrincipal();
                                context.HttpContext.SignOutAsync();
                                context.HttpContext.Response.Redirect("~");
                                return Task.FromResult(false);
                            }
                        }
                    };
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.LoginPath = new PathString("/Account");
                    options.ReturnUrlParameter = "RequestPath";
                    options.SlidingExpiration = true;
                });
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
