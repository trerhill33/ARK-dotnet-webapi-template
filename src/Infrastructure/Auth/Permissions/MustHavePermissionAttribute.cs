using ARK.WebApi.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace ARK.WebApi.Infrastructure.Auth.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = ARKPermission.NameFor(action, resource);
}