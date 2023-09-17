using System.Text;
using FcxLabsUserManagement.Core;
using FcxLabsUserManagement.Core.Contracts.Repositories;
using FcxLabsUserManagement.Core.Contracts.Services;
using FcxLabsUserManagement.Infra.Configs.Options;
using FcxLabsUserManagement.Infra.Repositories;
using FcxLabsUserManagement.Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FcxLabsUserManagement.Infra;

public static class DIExtension
{	
	public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration config)
	{
		//CORS
		services.AddCors(policyBuilder => policyBuilder.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200").AllowAnyOrigin().AllowAnyHeader()));
		
		//Config
		services.Configure<EmailConfigOptions>(config.GetSection("EmailConfig"));
		services.Configure<JwtOptions>(config.GetSection("JWT"));
		
		services.AddLogging();
		
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
		
		services.AddIdentity<UserIdentity, IdentityRole>(options => 
			{
			  options.User.RequireUniqueEmail = true;
			})
			.AddEntityFrameworkStores<UserDbContext>()
			.AddDefaultTokenProviders();
		
		services.Configure<IdentityOptions>(
			options => options.SignIn.RequireConfirmedEmail = true
		);
		
		var _jwtConfig = config.GetSection("JWT").Get<JwtOptions>();
		
		services.AddAuthentication(options => 
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			
		}).AddJwtBearer(jwtOptions => 
		{
			jwtOptions.TokenValidationParameters = new TokenValidationParameters
			{
				ValidIssuer = _jwtConfig.ValidIssuer,
				ValidAudience = _jwtConfig.ValidAudience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret)),
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateIssuerSigningKey = true,
				ValidateLifetime = true
			};
		});
		
		services.AddAuthorization();
		
		return services;
	}
}
