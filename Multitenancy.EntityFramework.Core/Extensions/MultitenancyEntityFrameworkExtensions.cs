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
        public static void UseEntityFrameworkCore(
            this MultitenancyBuilder<Tenant, int> builder, 
            Action<DbContextOptionsBuilder> options)
        {
            builder.Service.Replace(ServiceDescriptor.Scoped<ITenantRepository<Tenant, int>, TenantEntityFrameworkCoreRepository>());
            builder.Service.AddDbContext<TenantDbContext<Tenant, int>>(options);
            builder.Service.RunMigrations<Tenant, int>();
        }

        public static void UseEntityFrameworkCore<TTenant, TKey>(
            this MultitenancyBuilder<TTenant, TKey> builder,
            Action<DbContextOptionsBuilder> options)
            where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
        {
            builder.Service.Replace(ServiceDescriptor.Scoped<ITenantRepository<TTenant, TKey>, TenantEntityFrameworkCoreRepository<TTenant, TKey>>());
            builder.Service.AddDbContext<TenantDbContext<TTenant, TKey>>(options);
            builder.Service.RunMigrations<TTenant, TKey>();
        }

        private static void RunMigrations<TTenant, TKey>(this IServiceCollection service)
            where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
        {
            var context = service.BuildServiceProvider().GetService<TenantDbContext<TTenant, TKey>>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}