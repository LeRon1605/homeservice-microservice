using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BuildingBlocks.Presentation.Authorization;

public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.Contains('.'))
            return await base.GetPolicyAsync(policyName);

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName)).Build();
    }
}