using Microsoft.Extensions.DependencyInjection;
using Multitenancy.DataAccess.Contexts;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Extensions
{
    public static class MultitenancyBuilderExtensions
    {
        public static void EnableEntityFrameworkCore(this MultitenancyBuilder builder)
        {
            var service = builder.Service.BuildServiceProvider().GetService<TenantDbContext<Tenant, int>>();
            service.Database.EnsureDeleted();
            service.Database.EnsureCreated();
        }
    }
}