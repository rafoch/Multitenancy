using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CSharpFunctionalExtensions;
using Multitenancy.Common.Extensions;
using Multitenancy.Common.Exceptions;
using Multitenancy.Common.Interfaces;
using Multitenancy.Core.Services;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Repository
{
    public class TenantInMemoryRepository : TenantInMemoryRepository<Tenant, int>, ITenantRepository
    {
        public TenantInMemoryRepository() 
            : base()
        {
        }
    }

    public class TenantInMemoryRepository<TTenant, TKey> : ITenantRepository<TTenant, TKey> where TKey : IEquatable<TKey> where TTenant : Tenant<TKey>
    {
        private readonly List<TTenant> _tenantRepository;
        public TenantInMemoryRepository()
        {
            _tenantRepository = new List<TTenant>();
        }

        public TTenant GetTenant(TKey id)
        {
            var tenant = _tenantRepository.SingleOrDefault(t => t.Id.Equals(id));
            if (tenant is null)
            {
                throw new TenantNotFoundException(id.ToString());
            }
            return tenant;
        }

        public Result<TTenant> RegisterTenant(TTenant tenant)
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
            return Result.Success(tenant);
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
                tenant.Username,
                tenant.DatabaseName);

            _tenantRepository.Add(tenantToUpdate);

            return Result.Success();
        }

        public Result<string> GetTenantConnectionString(TKey id)
        {
            var tenant = _tenantRepository.AsQueryable().Filter(t => t.Id, id).SingleOrDefault();
            return tenant?.ConnectionString;
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