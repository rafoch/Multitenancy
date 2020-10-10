using System;
using CSharpFunctionalExtensions;
using Microsoft.Data.SqlClient;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Services
{
    internal class TenantConnectionStringBuilder : TenantConnectionStringBuilder<Tenant, int>
    {
    }

    internal class TenantConnectionStringBuilder<TTenant, TKey> : ITenantConnectionStringBuilder<TTenant, TKey> where TKey : IEquatable<TKey> where TTenant : Tenant<TKey>
    {
        public Result<string> GetTenantConnectionString(TTenant tenant)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder =
                new SqlConnectionStringBuilder
                {
                    Password = tenant.Password ?? String.Empty,
                    UserID = tenant.Username ?? String.Empty,
                    DataSource = tenant.ServerName ?? String.Empty + "," + tenant.Port ?? String.Empty
                };
            return Result.Success(sqlConnectionStringBuilder.ConnectionString);
        }
    }
}