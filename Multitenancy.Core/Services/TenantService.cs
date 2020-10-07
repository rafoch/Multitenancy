using System;
using System.Linq;
using CSharpFunctionalExtensions;
using Multitenancy.DataAccess.Contexts;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Services
{
    internal class TenantService : TenantService<Tenant, int>, ITenantService<Tenant>
    {
        public TenantService(TenantDbContext<Tenant, int> myTenantDbContext) : base(myTenantDbContext)
        {
        }

    }
    internal class TenantService<TTenant, TKey> : ITenantService<TTenant, TKey> where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
    {
        private readonly TenantDbContext<TTenant, TKey> _tenantDbContext;

        public TenantService(TenantDbContext<TTenant, TKey> tenantDbContext)
        {
            _tenantDbContext = tenantDbContext;
        }

        public void RegisterTenant(TTenant tenant)
        {
            _tenantDbContext.Tenants.Add(tenant);
            _tenantDbContext.SaveChanges();
        }

        public TTenant GetTenant()
        {
            var firstOrDefault = _tenantDbContext.Tenants.FirstOrDefault();
            return firstOrDefault;
        }
    }
}