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
            _tenantService.RegisterTenant(new Tenant
            {
                Name = "Name"
            });
            return new OkObjectResult(_tenantService.GetTenant());
        }
    }
}