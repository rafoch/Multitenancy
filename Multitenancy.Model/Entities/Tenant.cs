using System;
using Microsoft.Data.SqlClient;
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

        public string ConnectionString
        {
            get
            {
                var serverName = ServerName ?? String.Empty + "," + Port ?? String.Empty;
                var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
                {
                    Password = Password ?? String.Empty,
                    UserID = Username ?? String.Empty,
                    DataSource = serverName
                };
                return sqlConnectionStringBuilder.ConnectionString;
            }
        }

        internal void Update(
            string description,
            string name, 
            string serverName, 
            string port, 
            string username, 
            string password)
        {
            Description = description;
            Name = name;
            ServerName = serverName;
            Port = port;
            Username = username;
            Password = password;
        }
    }
}