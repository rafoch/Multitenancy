using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Multitenancy.Common.Builder;
using Multitenancy.Common.Interfaces;
using Multitenancy.EntityFramework.Core.DataAccess;
using Multitenancy.EntityFramework.Core.Repository;
using Multitenancy.Model.Entities;

namespace Multitenancy.EntityFramework.Core.Extensions
{
    public static class MultitenancyEntityFrameworkExtensions
    {
        public static void UseEntityFrameworkCore(this MultitenancyBuilder builder, Action<DbContextOptionsBuilder> options)
        {
            builder.Service.Replace(ServiceDescriptor.Scoped<ITenantRepository<Tenant, int>, TenantEntityFrameworkCoreRepository>());
            builder.Service.AddDbContext<TenantDbContext<Tenant, int>>(options);
            var service = builder.Service.BuildServiceProvider().GetService<TenantDbContext<Tenant, int>>();
            // service.Database.EnsureDeleted();
            // service.Database.EnsureCreated();
            builder.Service.RunMigrations<Tenant, int>();
        }

        private static void RunMigrations<TTenant, TKey>(this IServiceCollection service)
            where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
        {
            var context = service.BuildServiceProvider().GetService<TenantDbContext<TTenant, TKey>>();
            context.Database.EnsureCreated();
        }
    }
}