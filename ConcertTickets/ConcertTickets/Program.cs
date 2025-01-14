using ConcertTickets.Data;
using ConcertTickets.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConcertTickets
{
    public class Program
    {
        public static void Main(string[] args)
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
			SeedClaimsAsync(app.Services).GetAwaiter().GetResult();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.RunAsync();
        }

		static async Task SeedClaimsAsync(IServiceProvider serviceProvider)
		{
			const string ADMIN_ACCOUNT = "admin@test.be";
			const string ADMIN_PASSWORD = "Admin@123";
			using var scope = serviceProvider.CreateScope();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();

			var user = await userManager.FindByEmailAsync(ADMIN_ACCOUNT);
			if (user == null)
			{
				// Optioneel: maak een standaardgebruiker aan als die niet bestaat
				user = new CustomUser
				{
					FirstName = "Admin",
					LastName = "User",
					UserName = ADMIN_ACCOUNT,
					Email = ADMIN_ACCOUNT,
					EmailConfirmed = true
				};
				await userManager.CreateAsync(user, ADMIN_PASSWORD); // Standaard wachtwoord
			}

            // Voeg claims toe aan de gebruiker
            var claims = new[]
            {
        new Claim("IsAdmin", "true"),

           };

			foreach (var claim in claims)
			{
				if (!(await userManager.GetClaimsAsync(user)).Any(c => c.Type == claim.Type))
				{
					await userManager.AddClaimAsync(user, claim);
				}
			}
		}
	}
}
