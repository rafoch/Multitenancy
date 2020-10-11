using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CSharpFunctionalExtensions;
using Multitenancy.Common.EntityBase;
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

        public Result RegisterTenant(TTenant tenant)
        {
            _context.Tenants.Add(tenant);
            _context.SaveChanges();
            return Result.Success(tenant);
        }

        public Result UpdateTenant(TTenant tenant)
        {
            throw new NotImplementedException();
        }

        public Result<string> GetTenantConnectionString(TKey id)
        {
            throw new NotImplementedException();
        }

        public Result RemoveTenant(TKey id)
        {
            throw new NotImplementedException();
        }

        public Result<ReadOnlyCollection<TTenant>> GetAllTenants()
        {
            throw new NotImplementedException();
        }
    }
}