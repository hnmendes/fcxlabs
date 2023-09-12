using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FcxLabsUserManagement.Infra;

public static class DIExtension
{
	public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration config)
	{
		//DB
		
		services.AddDbContext<UserDbContext>(options => options.UseSqlServer(config.GetConnectionString("UserDB")));
		
		//Identity
		
		services.AddIdentity<UserIdentity, IdentityRole>()
				.AddEntityFrameworkStores<UserDbContext>()
				.AddDefaultTokenProviders();
		
		services.AddAuthentication(options => 
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		});
		
		return services;
	}
}
