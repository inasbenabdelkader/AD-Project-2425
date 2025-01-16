using ConcertTickets.Models.Entities;
using ConcertTickets.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConcertTickets.Data
{
	public class ApplicationDbContext : IdentityDbContext<CustomUser>
	{
		public DbSet<Concert> Concerts { get; set; }
		public DbSet<TicketOffer> TicketOffers { get; set; }
		public DbSet<Order> Orders { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configure CustomUser
			modelBuilder.Entity<CustomUser>(entity =>
			{
				entity.Property(u => u.FirstName)
					.HasMaxLength(50)
					.IsRequired(false);

				entity.Property(u => u.LastName)
					.HasMaxLength(50)
					.IsRequired(false);

				entity.Property(u => u.MemberCardNumber)
					.HasMaxLength(20)
					.IsRequired(false);
			});

			// Configure TicketOffer relationships
			modelBuilder.Entity<TicketOffer>()
				.HasOne(t => t.Concert)
				.WithMany(c => c.TicketOffers)
				.HasForeignKey(t => t.ConcertId)
				.OnDelete(DeleteBehavior.Cascade);

			// Add unique index for Concert
			modelBuilder.Entity<Concert>()
				.HasIndex(c => new { c.Artist, c.Location, c.Date })
				.IsUnique();

			// Seed data for Concerts
			modelBuilder.Entity<Concert>().HasData(
				new Concert { Id = 1, Artist = "Taylor Swift", Location = "Koning Boudewijn Stadion, Brussel", Date = new DateTime(2025, 3, 15) },
				new Concert { Id = 2, Artist = "Taylor Swift", Location = "Koning Boudewijn Stadion, Brussel", Date = new DateTime(2025, 3, 16) },
				new Concert { Id = 3, Artist = "Charli XCX", Location = "Vorst Nationaal, Brussel", Date = new DateTime(2025, 4, 16) },
				new Concert { Id = 4, Artist = "Compact Disk Dummies", Location = "Ancienne Belgique, Brussel", Date = new DateTime(2025, 4, 26) },
				new Concert { Id = 5, Artist = "Compact Disk Dummies", Location = "Ancienne Belgique, Brussel", Date = new DateTime(2025, 4, 27) },
				new Concert { Id = 6, Artist = "Coldplay", Location = "Sportpaleis, Antwerpen", Date = new DateTime(2025, 5, 7) },
				new Concert { Id = 7, Artist = "Dua Lipa", Location = "Werchter", Date = new DateTime(2025, 6, 18) }
			);

			// Seed data for TicketOffers
			modelBuilder.Entity<TicketOffer>().HasData(
				new TicketOffer { Id = 1, TicketType = "Golden Circle", NumTickets = 10, Price = 200, ConcertId = 1 },
				new TicketOffer { Id = 2, TicketType = "Standing", NumTickets = 50, Price = 50, ConcertId = 1 },
				new TicketOffer { Id = 3, TicketType = "Seated", NumTickets = 60, Price = 60, ConcertId = 1 },
				new TicketOffer { Id = 4, TicketType = "Golden Circle", NumTickets = 1000, Price = 200, ConcertId = 2 },
				new TicketOffer { Id = 5, TicketType = "Standing", NumTickets = 19000, Price = 50, ConcertId = 2 },
				new TicketOffer { Id = 6, TicketType = "Seated", NumTickets = 20000, Price = 60, ConcertId = 2 },
				new TicketOffer { Id = 10, TicketType = "Standing", NumTickets = 2000, Price = 28, ConcertId = 4 },
				new TicketOffer { Id = 11, TicketType = "Seated", NumTickets = 1800, Price = 32, ConcertId = 4 },
				new TicketOffer { Id = 12, TicketType = "Standing", NumTickets = 2000, Price = 28, ConcertId = 5 },
				new TicketOffer { Id = 13, TicketType = "Seated", NumTickets = 7800, Price = 32, ConcertId = 5 },
				new TicketOffer { Id = 14, TicketType = "Golden Circle", NumTickets = 400, Price = 150, ConcertId = 6 },
				new TicketOffer { Id = 15, TicketType = "Standing", NumTickets = 4000, Price = 65, ConcertId = 6 },
				new TicketOffer { Id = 16, TicketType = "Seated", NumTickets = 4000, Price = 55, ConcertId = 6 },
				new TicketOffer { Id = 17, TicketType = "Golden Circle", NumTickets = 1000, Price = 124, ConcertId = 7 },
				new TicketOffer { Id = 18, TicketType = "Standing", NumTickets = 20000, Price = 67, ConcertId = 7 }
			);
		}
	}
}
