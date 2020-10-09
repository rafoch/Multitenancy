using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Multitenancy.Common.EntityBase;
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
            return Filter(_context.Tenants, p=> p.Id, id).SingleOrDefault();
        }

     private static IQueryable<TEntity> Filter<TEntity, TProperty>(IQueryable<TEntity> dbSet,
            Expression<Func<TEntity, TProperty>> property,
            TProperty value)
        {

            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null || !(memberExpression.Member is PropertyInfo))
            {
                throw new ArgumentException("Property expected", "property");
            }

            Expression left = property.Body;
            Expression right = Expression.Constant(value, typeof(TProperty));

            Expression searchExpression = Expression.Equal(left, right);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(left, right),
                new ParameterExpression[] { property.Parameters.Single() });

            return dbSet.Where(lambda);
        }
    }
}