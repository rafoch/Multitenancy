using System;
using Microsoft.EntityFrameworkCore;
using Multitenancy.Model.Entities;

namespace Multitenancy.DataAccess.Contexts
{
    internal class TenantDbContext : TenantDbContext<Tenant, int>
    {
        public TenantDbContext(DbContextOptions<TenantDbContext<Tenant, int>> options) : base(options)
        {
        }
    }

    internal class TenantDbContext<TTenant, TKey> : DbContext where TTenant : Tenant<TKey> where TKey : IEquatable<TKey>
    {
        public TenantDbContext(DbContextOptions<TenantDbContext<TTenant, TKey>> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        internal DbSet<TTenant> Tenants { get; set; }
    }
}