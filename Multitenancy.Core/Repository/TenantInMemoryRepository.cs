using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Multitenancy.Common.Interfaces;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Repository
{
    public class TenantInMemoryRepository : TenantInMemoryRepository<Tenant, int>
    {

    }

    public class TenantInMemoryRepository<TTenant, TKey> : ITenantRepository<TTenant, TKey> where TKey : IEquatable<TKey> where TTenant : Tenant<TKey>
    {
        public readonly List<TTenant> _tenantRepository;
        public TenantInMemoryRepository()
        {
            _tenantRepository = new List<TTenant>();
        }

        public TTenant GetTenant(TKey id)
        {
            return _tenantRepository.SingleOrDefault(t => t.Id.Equals(id));
        }

        public Result RegisterTenant(TTenant tenant)
        {
            _tenantRepository.Add(tenant);
            return Result.Success(tenant);
        }
    }
}