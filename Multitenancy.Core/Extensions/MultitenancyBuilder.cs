﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace Multitenancy.Core.Extensions
{
    public class MultitenancyBuilder
    {
        public MultitenancyBuilder(
            Type tenantType,
            Type keyType,
            IServiceCollection service)
        {
            TenantType = tenantType;
            KeyType = keyType;
            Service = service;
        }

        public Type TenantType { get; set; }
        public Type KeyType { get; set; }
        public IServiceCollection Service { get; }
    }
}