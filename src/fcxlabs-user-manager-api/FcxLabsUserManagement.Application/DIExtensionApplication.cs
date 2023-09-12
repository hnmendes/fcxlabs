using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FcxLabsUserManagement.Application;

public static class DIExtensionApplication
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		return services;
	}
}
