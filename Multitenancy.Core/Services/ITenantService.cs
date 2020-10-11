using System;
using System.Collections.ObjectModel;
using CSharpFunctionalExtensions;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Services
{
    public interface ITenantService<TTenant> : ITenantService<TTenant, int> where TTenant : Tenant
    {
    }

    public interface ITenantService<TTenant, TKey> where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
    {
        Result<ReadOnlyCollection<TTenant>> GetAllTenants();

        Result RegisterTenant(TTenant tenant);
        Result UpdateTenant(TTenant tenant);
        TTenant GetTenant(TKey id);
        Result<string> GetTenantConnectionString(TKey id);

        Result RemoveTenant(TKey id);
    }
}