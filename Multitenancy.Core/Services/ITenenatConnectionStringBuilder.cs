using System;
using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.Data.SqlClient;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Services
{
    internal interface ITenantConnectionStringBuilder<TTenant> : ITenantConnectionStringBuilder<TTenant, int>
        where TTenant : Tenant
    {
    }

    public interface ITenantConnectionStringBuilder<TTenant, TKey>
        where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
    {
        Result<string> GetTenantConnectionString(TTenant tenant);
    }
}