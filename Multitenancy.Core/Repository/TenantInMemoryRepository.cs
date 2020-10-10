using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CSharpFunctionalExtensions;
using Multitenancy.Common.Interfaces;
using Multitenancy.Core.Services;
using Multitenancy.Model.Entities;

namespace Multitenancy.Core.Repository
{
    public class TenantInMemoryRepository : TenantInMemoryRepository<Tenant, int>
    {
        public TenantInMemoryRepository(ITenantConnectionStringBuilder<Tenant, int> tenantConnectionStringBuilder) 
            : base(tenantConnectionStringBuilder)
        {
        }
    }

    public class TenantInMemoryRepository<TTenant, TKey> : ITenantRepository<TTenant, TKey> where TKey : IEquatable<TKey> where TTenant : Tenant<TKey>
    {
        private readonly ITenantConnectionStringBuilder<TTenant, TKey> _tenantConnectionStringBuilder;
        public readonly List<TTenant> _tenantRepository;
        public TenantInMemoryRepository(
            ITenantConnectionStringBuilder<TTenant, TKey> tenantConnectionStringBuilder)
        {
            _tenantConnectionStringBuilder = tenantConnectionStringBuilder;
            _tenantRepository = new List<TTenant>();
        }

        public TTenant GetTenant(TKey id)
        {
            return _tenantRepository.SingleOrDefault(t => t.Id.Equals(id));
        }

        public Result RegisterTenant(TTenant tenant)
        {
            _tenantRepository.Add(tenant);
            return Result.Success();
        }

        public Result UpdateTenant(TTenant tenant)
        {
            var tenantToUpdate = Filter(_tenantRepository.AsQueryable(), t => t.Id, tenant.Id).SingleOrDefault();
            if (tenantToUpdate is null)
            {
                return Result.Failure("Tenant does not exists in database");
            }

            _tenantRepository.Remove(tenantToUpdate);

            tenantToUpdate.Update(
                tenant.Description,
                tenant.Name,
                tenant.Password,
                tenant.Port,
                tenant.ServerName,
                tenant.Username);

            _tenantRepository.Add(tenantToUpdate);

            return Result.Success();
        }

        public Result<string> GetTenantConnectionString(TKey id)
        {
            var tenant = Filter(_tenantRepository.AsQueryable(), t => t.Id, id).SingleOrDefault();
            return _tenantConnectionStringBuilder.GetTenantConnectionString(tenant);
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