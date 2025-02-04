using ConcertTickets.Data;
using ConcertTickets.Models.Identity;
using ConcertTickets.Repositories;
using ConcertTickets.Repositories.Interfaces;
using ConcertTickets.Services;
using ConcertTickets.Services.Interfaces;
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

			// Add services to the container
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddDefaultIdentity<CustomUser>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 6;
			})
				.AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddRazorPages();

			// Add Authorization Policy
			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminOnly", policy =>
					policy.RequireClaim("IsAdmin", "true"));
			});

			// Add Services
			builder.Services.AddScoped<IConcertService, ConcertService>();
			builder.Services.AddScoped<ITicketOfferService, TicketOfferService>();
			builder.Services.AddScoped<IOrderService, OrderService>();

			// Add Repositories
			builder.Services.AddScoped<IConcertRepository, ConcertRepository>();
			builder.Services.AddScoped<ITicketOfferRepository, TicketOfferRepository>();
			builder.Services.AddScoped<IOrderRepository, OrderRepository>();

			var app = builder.Build();

			// Seed initial admin claims
			SeedClaimsAsync(app.Services).GetAwaiter().GetResult();

			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Concert}/{action=Index}/{id?}");
			app.MapRazorPages();

			app.Run();
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
				user = new CustomUser
				{
					FirstName = "Admin",
					LastName = "User",
					UserName = ADMIN_ACCOUNT,
					Email = ADMIN_ACCOUNT,
					EmailConfirmed = true
				};
				await userManager.CreateAsync(user, ADMIN_PASSWORD);
			}

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