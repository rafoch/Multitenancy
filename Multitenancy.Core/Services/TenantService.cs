using System;
using System.Collections.ObjectModel;
using CSharpFunctionalExtensions;
using Multitenancy.Common.Interfaces;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Services
{
    internal class TenantService : TenantService<Tenant, int>, ITenantService
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

        public Result<ReadOnlyCollection<TTenant>> GetAllTenants()
        {
            return _tenantRepository.GetAllTenants();
        }

        public Result RegisterTenant(TTenant tenant)
        {
            return _tenantRepository.RegisterTenant(tenant);
        }

        public Result UpdateTenant(TTenant tenant)
        {
            return _tenantRepository.UpdateTenant(tenant);
        }

        public TTenant GetTenant(TKey id)
        {
            return _tenantRepository.GetTenant(id);
        }

        public Result<string> GetTenantConnectionString(TKey id)
        {
            return _tenantRepository.GetTenantConnectionString(id);
        }

        public Result RemoveTenant(TKey id)
        {
            return _tenantRepository.RemoveTenant(id);
        }
    }
}