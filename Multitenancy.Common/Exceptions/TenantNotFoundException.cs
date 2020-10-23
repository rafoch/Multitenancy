using System;

namespace Multitenancy.Common.Exceptions
{
    public class TenantNotFoundException : Exception
    {
        private new const string Message = "Tenant not found on database with id {0}";
        public TenantNotFoundException()
        {
        }

        public TenantNotFoundException(string tenantId)
            :base(string.Format(Message, tenantId))
        {
        }

        public TenantNotFoundException(string tenantId, Exception inner)
            :base(string.Format(Message, tenantId), inner)
        {
        }
    }

    public class TenantCannotBeSavedException : Exception
    {
        private new const string Message = "Cannot save tenant data: {0}";
        public TenantCannotBeSavedException()
        {
        }

        public TenantCannotBeSavedException(string tenantData)
            :base(string.Format(Message, tenantData))
        {
        }

        public TenantCannotBeSavedException(string tenantData, Exception inner)
            :base(string.Format(Message, tenantData), inner)
        {
        }
    }
}