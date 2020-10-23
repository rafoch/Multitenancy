using System;
using Microsoft.AspNetCore.Mvc;
using Multitenancy.Common.Interfaces;
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
            var myTenant = new MyTenant
            {
                Name = "Name"
            };
            _myTenantService.RegisterTenant(myTenant);
    
            return new OkObjectResult(_myTenantService.GetTenant(myTenant.Id));
        }
    }
}