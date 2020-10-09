using Microsoft.Extensions.DependencyInjection;
using Multitenancy.Common.Builder;
using Multitenancy.EntityFramework.Core.DataAccess;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Extensions
{
    public static class MultitenancyBuilderExtensions
    {
        // public static void UseEntityFrameworkCore(this MultitenancyBuilder builder)
        // {
        //     var service = builder.Service.BuildServiceProvider().GetService<TenantDbContext<Tenant, int>>();
        //     service.Database.EnsureDeleted();
        //     service.Database.EnsureCreated();
        // }
    }
}