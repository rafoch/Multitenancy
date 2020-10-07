using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Multitenancy.Core.Services;
using Multitenancy.DataAccess.Contexts;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static MultitenancyBuilder AddMultitenancy(this IServiceCollection service, Action<DbContextOptionsBuilder> options)
        {
            service.AddDbContext<TenantDbContext<Tenant,int>>(options);
            service.TryAddScoped<ITenantService<Tenant>, TenantService>();
            // service.RunMigrations<Tenant,int>();
            return new MultitenancyBuilder(typeof(Tenant), typeof(int), service);
        }

        public static void AddMultitenancy<TTenant, TKey>(this IServiceCollection service, Action<DbContextOptionsBuilder> options)
            where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
        {
            service.TryAddScoped<ITenantService<TTenant, TKey>, TenantService<TTenant, TKey>>();
            service.AddDbContext<TenantDbContext<TTenant, TKey>>(options);
            service.RunMigrations<TTenant, TKey>();
        }

        private static void RunMigrations<TTenant, TKey>(this IServiceCollection service)
            where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
        {
            var context = service.BuildServiceProvider().GetService<TenantDbContext<TTenant, TKey>>();
            context.Database.EnsureCreated();
        }
    }
}