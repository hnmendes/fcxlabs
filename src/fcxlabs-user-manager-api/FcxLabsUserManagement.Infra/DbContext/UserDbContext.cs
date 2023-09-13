using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FcxLabsUserManagement.Infra
{
	public class UserDbContext : IdentityDbContext<UserIdentity> 
	{
		public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			SeedRoles(builder);
			AddUniqueColumns(builder);
		}
		
		public override Task<int> SaveChangesAsync(CancellationToken cto = default)
		{
			foreach(var entry in ChangeTracker.Entries<UserIdentity>())
			{
				switch(entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedOn = DateTime.Now;
						break;
					case EntityState.Modified:
						entry.Entity.ModifiedOn = DateTime.Now;
						break;
				}
			}
			return base.SaveChangesAsync(cto);
		}
		
		private static void SeedRoles(ModelBuilder builder)
		{
			builder.Entity<IdentityRole>().HasData(
				new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
				new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }	
			);
		}
		
		private static void AddUniqueColumns(ModelBuilder builder)
		{
			builder.Entity<UserIdentity>()
				.HasIndex(u => u.CPF)
				.IsUnique();
		}
	}
}
