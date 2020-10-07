using System;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Services
{
    public interface ITenantService<TTenant> : ITenantService<TTenant, int> where TTenant : Tenant
    {

    }

    public interface ITenantService<TTenant, TKey> where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
    {
        void RegisterTenant(TTenant tenant);
        TTenant GetTenant();
    }
}