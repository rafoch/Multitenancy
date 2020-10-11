using System;
using System.Collections.ObjectModel;
using CSharpFunctionalExtensions;
using Multitenancy.Model.Entities;

namespace Multitenancy.Common.Interfaces
{
    public interface ITenantRepository : ITenantRepository<Tenant, int>
    {
    }

    public interface ITenantRepository<TTenant, in TKey> where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Method provides tenant object by it's id.
        /// </summary>
        /// <param name="id">Tenant id</param>
        /// <returns>Tenant object</returns>
        TTenant GetTenant(TKey id);
        /// <summary>
        /// Method allows user to create tenant in store and saves it.
        /// </summary>
        /// <param name="tenant">Tenant object</param>
        /// <returns>Result of operation</returns>
        Result RegisterTenant(TTenant tenant);
        /// <summary>
        /// Update existing Tenant object.
        /// </summary>
        /// <param name="tenant">Tenant object without changing it's id - id must be the same becouse method is searching by <para>Id</para> parameter.</param>
        /// <returns>Result of operation</returns>
        Result UpdateTenant(TTenant tenant);

        /// <summary>
        /// Get connection string to database for specific tenant.
        /// </summary>
        /// <param name="id">Tenant id</param>
        /// <returns>String result, which contains connection string to the database.</returns>
        Result<string> GetTenantConnectionString(TKey id);
        /// <summary>
        /// Removes Tenant from the store.
        /// </summary>
        /// <param name="id">Tenant id.</param>
        /// <returns>Result of operation.</returns>
        Result RemoveTenant(TKey id);

        Result<ReadOnlyCollection<TTenant>> GetAllTenants();
    }
}
