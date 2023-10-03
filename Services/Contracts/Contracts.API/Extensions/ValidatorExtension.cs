using Contracts.Application;
using FluentValidation;

namespace Contracts.API.Extensions;

public static class ValidatorExtension
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
	{
		services.AddValidatorsFromAssemblyContaining(typeof(IContractApplicationMarker));
		
		return services;
	}
}