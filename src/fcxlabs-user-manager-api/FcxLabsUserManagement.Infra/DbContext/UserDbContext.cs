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
		}
		
		private static void SeedRoles(ModelBuilder builder)
		{
			builder.Entity<IdentityRole>().HasData(
				new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
				new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }	
			);
		}
	}
}
