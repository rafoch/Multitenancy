using System;
using Multitenancy.Common.Interfaces;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Services
{
    internal class TenantService : TenantService<Tenant, int>, ITenantService<Tenant>
    {
        public TenantService(ITenantRepository<Tenant, int> myTenantDbContext) : base(myTenantDbContext)
        {
        }

    }
    internal class TenantService<TTenant, TKey> : ITenantService<TTenant, TKey> where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
    {
        private readonly ITenantRepository<TTenant, TKey> _tenantRepository;

        public TenantService(ITenantRepository<TTenant, TKey> tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public void RegisterTenant(TTenant tenant)
        {
            _tenantRepository.RegisterTenant(tenant);
        }

        public TTenant GetTenant(TKey id)
        {
            var firstOrDefault = _tenantRepository.GetTenant(id);
            return firstOrDefault;
        }
    }
}