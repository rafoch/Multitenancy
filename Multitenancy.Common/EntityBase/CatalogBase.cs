namespace Multitenancy.Common.EntityBase
{
    interface ITenantBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ServerName { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}