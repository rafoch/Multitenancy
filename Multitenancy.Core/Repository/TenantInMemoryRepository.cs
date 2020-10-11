using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CSharpFunctionalExtensions;
using Multitenancy.Common.Extensions;
using Multitenancy.Common.Interfaces;
using Multitenancy.Core.Services;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Repository
{
    public class TenantInMemoryRepository : TenantInMemoryRepository<Tenant, int>
    {
        public TenantInMemoryRepository(ITenantConnectionStringBuilder<Tenant, int> tenantConnectionStringBuilder) 
            : base(tenantConnectionStringBuilder)
        {
        }
    }

    public class TenantInMemoryRepository<TTenant, TKey> : ITenantRepository<TTenant, TKey> where TKey : IEquatable<TKey> where TTenant : Tenant<TKey>
    {
        private readonly ITenantConnectionStringBuilder<TTenant, TKey> _tenantConnectionStringBuilder;
        private readonly List<TTenant> _tenantRepository;
        public TenantInMemoryRepository(
            ITenantConnectionStringBuilder<TTenant, TKey> tenantConnectionStringBuilder)
        {
            _tenantConnectionStringBuilder = tenantConnectionStringBuilder;
            _tenantRepository = new List<TTenant>();
        }

        public TTenant GetTenant(TKey id)
        {
            return _tenantRepository.SingleOrDefault(t => t.Id.Equals(id));
        }

        public Result RegisterTenant(TTenant tenant)
        {
            if (typeof(TKey) == typeof(int))
            {
                var maxId = _tenantRepository.Count;
                var id = maxId+1;
                var changeType = (TKey) Convert.ChangeType(id, typeof(TKey));
                tenant.Id = changeType;
                _tenantRepository.Add(tenant);
            }
            else
            {
                _tenantRepository.Add(tenant);
            }
            return Result.Success();
        }

        public Result UpdateTenant(TTenant tenant)
        {
            var tenantToUpdate = _tenantRepository.AsQueryable().Filter(t => t.Id, tenant.Id).SingleOrDefault();
            if (tenantToUpdate is null)
            {
                return Result.Failure("Tenant does not exists in database");
            }

            _tenantRepository.Remove(tenantToUpdate);

            tenantToUpdate.Update(
                tenant.Description,
                tenant.Name,
                tenant.Password,
                tenant.Port,
                tenant.ServerName,
                tenant.Username);

            _tenantRepository.Add(tenantToUpdate);

            return Result.Success();
        }

        public Result<string> GetTenantConnectionString(TKey id)
        {
            var tenant = _tenantRepository.AsQueryable().Filter(t => t.Id, id).SingleOrDefault();
            return _tenantConnectionStringBuilder.GetTenantConnectionString(tenant);
        }

        public Result RemoveTenant(TKey id)
        {
            var tenant = _tenantRepository.AsQueryable().Filter(t => t.Id, id).SingleOrDefault();
            _tenantRepository.Remove(tenant);
            return Result.Success();
        }

        public Result<ReadOnlyCollection<TTenant>> GetAllTenants()
        {
            return Result.Success(_tenantRepository.AsReadOnly());
        }
    }
}