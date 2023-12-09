using System.Collections.ObjectModel;

namespace ARK.WebApi.Shared.Authorization;

public static class ARKAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class ARKResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);
}

public static class ARKPermissions
{
    private static readonly ARKPermission[] _all = new ARKPermission[]
    {
        new("View Dashboard", ARKAction.View, ARKResource.Dashboard),
        new("View Hangfire", ARKAction.View, ARKResource.Hangfire),
        new("View Users", ARKAction.View, ARKResource.Users),
        new("Search Users", ARKAction.Search, ARKResource.Users),
        new("Create Users", ARKAction.Create, ARKResource.Users),
        new("Update Users", ARKAction.Update, ARKResource.Users),
        new("Delete Users", ARKAction.Delete, ARKResource.Users),
        new("Export Users", ARKAction.Export, ARKResource.Users),
        new("View UserRoles", ARKAction.View, ARKResource.UserRoles),
        new("Update UserRoles", ARKAction.Update, ARKResource.UserRoles),
        new("View Roles", ARKAction.View, ARKResource.Roles),
        new("Create Roles", ARKAction.Create, ARKResource.Roles),
        new("Update Roles", ARKAction.Update, ARKResource.Roles),
        new("Delete Roles", ARKAction.Delete, ARKResource.Roles),
        new("View RoleClaims", ARKAction.View, ARKResource.RoleClaims),
        new("Update RoleClaims", ARKAction.Update, ARKResource.RoleClaims),
        new("View Products", ARKAction.View, ARKResource.Products, IsBasic: true),
        new("Search Products", ARKAction.Search, ARKResource.Products, IsBasic: true),
        new("Create Products", ARKAction.Create, ARKResource.Products),
        new("Update Products", ARKAction.Update, ARKResource.Products),
        new("Delete Products", ARKAction.Delete, ARKResource.Products),
        new("Export Products", ARKAction.Export, ARKResource.Products),
        new("View Brands", ARKAction.View, ARKResource.Brands, IsBasic: true),
        new("Search Brands", ARKAction.Search, ARKResource.Brands, IsBasic: true),
        new("Create Brands", ARKAction.Create, ARKResource.Brands),
        new("Update Brands", ARKAction.Update, ARKResource.Brands),
        new("Delete Brands", ARKAction.Delete, ARKResource.Brands),
        new("Generate Brands", ARKAction.Generate, ARKResource.Brands),
        new("Clean Brands", ARKAction.Clean, ARKResource.Brands),
        new("View Tenants", ARKAction.View, ARKResource.Tenants, IsRoot: true),
        new("Create Tenants", ARKAction.Create, ARKResource.Tenants, IsRoot: true),
        new("Update Tenants", ARKAction.Update, ARKResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", ARKAction.UpgradeSubscription, ARKResource.Tenants, IsRoot: true)
    };

    public static IReadOnlyList<ARKPermission> All { get; } = new ReadOnlyCollection<ARKPermission>(_all);
    public static IReadOnlyList<ARKPermission> Root { get; } = new ReadOnlyCollection<ARKPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<ARKPermission> Admin { get; } = new ReadOnlyCollection<ARKPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<ARKPermission> Basic { get; } = new ReadOnlyCollection<ARKPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record ARKPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
