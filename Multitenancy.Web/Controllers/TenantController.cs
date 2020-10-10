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
        public ActionResult Get([FromRoute] int id)
        {
            return new OkObjectResult(_tenantService.GetTenant(id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] Tenant tenant)
        {
            return new OkObjectResult(_tenantService.RegisterTenant(tenant));
        }

        [HttpPut]
        public ActionResult Edit([FromBody] Tenant tenant)
        {
            return new OkObjectResult(_tenantService.UpdateTenant(tenant));
        }

        [HttpGet]
        public ActionResult GetConnection([FromRoute] int id)
        {
            var tenantConnectionString = _tenantService.GetTenantConnectionString(id);
            return new OkObjectResult(tenantConnectionString.Value);
        }
    }
}