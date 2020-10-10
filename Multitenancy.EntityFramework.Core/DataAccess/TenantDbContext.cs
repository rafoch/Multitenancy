using System;
using Microsoft.EntityFrameworkCore;
using Multitenancy.Model.Entities;

namespace Multitenancy.EntityFramework.Core.DataAccess
{
    internal class TenantDbContext<TTenant, TKey> : DbContext where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
    {
        public TenantDbContext(DbContextOptions<TenantDbContext<TTenant, TKey>> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Multitenancy");
            base.OnModelCreating(modelBuilder);
        }

        internal DbSet<TTenant> Tenants { get; set; }
    }
}