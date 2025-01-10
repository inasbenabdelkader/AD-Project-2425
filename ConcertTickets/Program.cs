using ConcertTickets.Data;
using ConcertTickets.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConcertTickets
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            });

            var app = builder.Build();

            // Create admin user
            await CreateAdminUserAsync(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication(); // Add this line
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            await app.RunAsync();
        }

        private static async Task CreateAdminUserAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();
                var admin = await userManager.FindByEmailAsync("admin@odisee.be");

                if (admin == null)
                {
                    admin = new CustomUser
                    {
                        UserName = "admin@odisee.be",
                        Email = "admin@odisee.be",
                        FirstName = "Admin",
                        LastName = "User",
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(admin, "Admin123!");
                    await userManager.AddClaimAsync(admin, new Claim("IsAdmin", "true"));
                }
            }
        }
    }
}
