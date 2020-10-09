using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Multitenancy.Common.Builder;
using Multitenancy.Common.Interfaces;
using Multitenancy.Core.Repository;
using Multitenancy.Core.Services;
using Multitenancy.EntityFramework.Core.DataAccess;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static MultitenancyBuilder AddMultitenancy(this IServiceCollection service)
        {
            service.TryAddScoped<ITenantService<Tenant>, TenantService>();
            service.TryAddScoped<ITenantRepository<Tenant, int>, TenantInMemoryRepository>();
            return new MultitenancyBuilder(typeof(Tenant), typeof(int), service);
        }

        public static MultitenancyBuilder AddMultitenancy<TTenant, TKey>(this IServiceCollection service)
            where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
        {
            service.TryAddScoped<ITenantService<TTenant, TKey>, TenantService<TTenant, TKey>>();
            service.TryAddScoped<ITenantRepository<TTenant, TKey>, TenantInMemoryRepository<TTenant, TKey>>();
            return new MultitenancyBuilder(typeof(TTenant), typeof(TKey), service);
        }
    }
}