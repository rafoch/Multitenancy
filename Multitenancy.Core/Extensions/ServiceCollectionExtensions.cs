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
        public static MultitenancyBuilder<Tenant, int> AddMultitenancy(this IServiceCollection service)
        {
            service.TryAddScoped<ITenantService, TenantService>();
            service.TryAddSingleton<ITenantRepository, TenantInMemoryRepository>();
            return new MultitenancyBuilder<Tenant, int>(typeof(Tenant), typeof(int), service);
        }

        public static MultitenancyBuilder<TTenant, TKey> AddMultitenancy<TTenant, TKey>(this IServiceCollection service)
            where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
        {
            service.TryAddScoped<ITenantService<TTenant, TKey>, TenantService<TTenant, TKey>>();
            service.TryAddScoped<ITenantRepository<TTenant, TKey>, TenantInMemoryRepository<TTenant, TKey>>();
            return new MultitenancyBuilder<TTenant, TKey>(typeof(TTenant), typeof(TKey), service);
        }
    }
}