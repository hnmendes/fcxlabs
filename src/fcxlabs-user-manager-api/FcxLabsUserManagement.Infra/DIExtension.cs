using System.Collections.Immutable;
using FcxLabsUserManagement.Core.Contracts.Repositories;
using FcxLabsUserManagement.Core.Contracts.Services;
using FcxLabsUserManagement.Infra.Configurations;
using FcxLabsUserManagement.Infra.Repositories;
using FcxLabsUserManagement.Infra.Services;
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
		//Config
		
		var emailConfig = config.GetSection(nameof(EmailConfig)).Get<EmailConfig>();
		services.AddSingleton(emailConfig);
		
		//DB
		
		services.AddDbContext<UserDbContext>(options => options.UseSqlServer(config.GetConnectionString("UserDB")));
		
		//Repositories
		
		services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
		services.AddScoped<IUserRepository, UserRepository>();
		
		//Services
		
		services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IEmailService, EmailService>();

		//Identity
		
		services.AddIdentity<UserIdentity, IdentityRole>()
				.AddEntityFrameworkStores<UserDbContext>()
				.AddDefaultTokenProviders();
		
		services.Configure<IdentityOptions>(
			options => options.SignIn.RequireConfirmedEmail = true
		);
		
		services.AddAuthentication(options => 
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		});
		
		return services;
	}
}
