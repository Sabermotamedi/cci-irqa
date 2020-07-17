using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Presentation.Data;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class CompanyController : Controller
    {
        ApplicationDbContext _dbContext;

        public CompanyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {

            var res = _dbContext.Organizations.Select(x => new KeyValueModel { Id = int.Parse(x.business_id), Name = x.business_type }).Distinct().ToList();
            ViewBag.result = res;


            return View();
        }


        [HttpPost]
        public JsonResult GetSubcategory([FromBody]RequestBusinesID requestBusinesID)
        {
            var res = _dbContext.Organizations.Where(x => x.business_id == requestBusinesID.Id.ToString()).Select(x => new KeyValueModel1 { organizationId = (x.organization_id), organizationtype = x.organization_type }).ToList();

            return Json(res);

        }

        [HttpPost]
        public JsonResult GetCompanies([FromBody]RequestOrgBisId requestOrgBisId)

        {


            List<Company> company = new List<Company>();
            company = _dbContext.Companies.Where(x => x.Business_Type == requestOrgBisId.businessid.ToString()
            && x.Organization_Type == requestOrgBisId.organizationid.ToString()).ToList();



            return Json(company);

        }

    }
    public class KeyValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class KeyValueModel1
    {
        public string organizationId { get; set; }
        public string organizationtype { get; set; }
    }

    public class RequestBusinesID
    {
        public int Id { get; set; }

    }
    public class RequestOrganizationId
    {
        public int Id { get; set; }

    }
    public class RequestOrgBisId
    {
        public int businessid { get; set; }
        public int organizationid { get; set; }
    }
}