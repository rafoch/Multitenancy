using System;
using Microsoft.AspNetCore.Mvc;
using Multitenancy.Core.Services;

namespace Multitenancy.Web.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ITenantService<MyTenant, Guid> _myTenantService;
    
        public CatalogController(ITenantService<MyTenant, Guid> myTenantService)
        {
            _myTenantService = myTenantService;
        }
    
        [HttpGet]
        public IActionResult GetTwo()
        {
            _myTenantService.RegisterTenant(new MyTenant
            {
                Name = "Name"
            });
    
            return new OkObjectResult(_myTenantService.GetTenant(Guid.NewGuid()));
        }
    }
}