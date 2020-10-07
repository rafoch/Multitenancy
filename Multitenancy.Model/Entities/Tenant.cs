using System;
using Multitenancy.Common.EntityBase;

namespace Multitenancy.Model.Entities
{
    public class Tenant : Tenant<int>
    {
    }

    public class Tenant<TKey> : BaseEntity<TKey> , ITenantBase where TKey : IEquatable<TKey>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ServerName { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}