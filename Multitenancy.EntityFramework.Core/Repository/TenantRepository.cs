using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CSharpFunctionalExtensions;
using Multitenancy.Common.EntityBase;
using Multitenancy.Common.Exceptions;
using Multitenancy.Common.Extensions;
using Multitenancy.Common.Interfaces;
using Multitenancy.EntityFramework.Core.DataAccess;
using Multitenancy.Model.Entities;

namespace Multitenancy.EntityFramework.Core.Repository
{
    internal class TenantEntityFrameworkCoreRepository : TenantEntityFrameworkCoreRepository<Tenant, int>, ITenantRepository
    {
        public TenantEntityFrameworkCoreRepository(TenantDbContext<Tenant, int> context) : base(context)
        {
        }
    }

    internal class TenantEntityFrameworkCoreRepository<TTenant, TKey> : ITenantRepository<TTenant, TKey> where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
    {
        private readonly TenantDbContext<TTenant, TKey> _context;

        public TenantEntityFrameworkCoreRepository(TenantDbContext<TTenant, TKey> context)
        {
            _context = context;
        }

        public TTenant GetTenant(TKey id)
        {
            return _context.Tenants.Filter(p=> p.Id, id).SingleOrDefault();
        }

        public Result<TTenant> RegisterTenant(TTenant tenant)
        {
            try
            {
                _context.Tenants.Add(tenant);
                _context.SaveChanges();
                return Result.Success(tenant);
            }
            catch (Exception e)
            {
                throw new TenantCannotBeSavedException(tenant?.ToString(), e);
            }
        }

        public Result UpdateTenant(TTenant tenant)
        {
            var tenantToUpdate = GetTenant(tenant.Id);
            tenantToUpdate.Update(
                tenant.Description,
                tenant.Name,
                tenant.ServerName,
                tenant.Port,
                tenant.Username,
                tenant.Password, 
                tenant.DatabaseName);
            _context.SaveChanges();
            return Result.Success();
        }

        public Result<string> GetTenantConnectionString(TKey id)
        {
            var tenant = GetTenant(id);
            return tenant.ConnectionString;
        }

        public Result RemoveTenant(TKey id)
        {
            var tenant = GetTenant(id);
            _context.Tenants.Remove(tenant);
            _context.SaveChanges();
            return Result.Success();
        }

        public Result<ReadOnlyCollection<TTenant>> GetAllTenants()
        {
            var tenants = _context.Tenants.ToList().AsReadOnly();
            return Result.Success(tenants);
        }
    }
}