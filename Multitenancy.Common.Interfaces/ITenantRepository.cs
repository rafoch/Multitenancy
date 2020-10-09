using System;
using CSharpFunctionalExtensions;
using Multitenancy.Model.Entities;

namespace Multitenancy.Common.Interfaces
{
    public interface ITenantRepository : ITenantRepository<Tenant, int>
    {
    }

    public interface ITenantRepository<TTenant, TKey> where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
    {
        TTenant GetTenant(TKey id);
        Result RegisterTenant(TTenant tenant);
    }
}
