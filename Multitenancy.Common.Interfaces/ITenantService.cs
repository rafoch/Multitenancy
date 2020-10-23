using System.Collections.ObjectModel;
using CSharpFunctionalExtensions;
using Multitenancy.Model.Entities;

namespace Multitenancy.Common.Interfaces
{
    public interface ITenantService : ITenantService<Tenant, int> { }

    public interface ITenantService<TTenant, in TKey> where TTenant : class
    {
        Result<ReadOnlyCollection<TTenant>> GetAllTenants();

        Result RegisterTenant(TTenant tenant);
        Result UpdateTenant(TTenant tenant);
        TTenant GetTenant(TKey id);
        Result<string> GetTenantConnectionString(TKey id);

        Result RemoveTenant(TKey id);
    }
}