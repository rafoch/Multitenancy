using Microsoft.AspNetCore.Mvc;
using Multitenancy.Core.Services;
using Multitenancy.Model.Entities;

namespace Multitenancy.Web.Controllers
{
    public class TenantController : Controller
    {
        private readonly ITenantService<Tenant> _tenantService;

        public TenantController(ITenantService<Tenant> tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var tenant = new Tenant
            {
                Name = "Name"
            };
            _tenantService.RegisterTenant(tenant);
            return new OkObjectResult(_tenantService.GetTenant(1));
        }
    }
}