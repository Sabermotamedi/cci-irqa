using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Presentation.Data;
using Presentation.Models;
using Presentation.Models.ResponseModels;

namespace Presentation.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult AddBusinesType()
        {
            return View();
        }
        public ActionResult GetAllCategory()
        {
            var res = _dbContext.Organizations.Select(x => new KeyValueModel { Id = int.Parse(x.business_id), Name = x.business_type }).Distinct().ToList();
            ViewBag.result = res;
            return View();
        }
        [HttpPost]
        public JsonResult AllSubCategory([FromBody]RequestBusinesID requestBusinesID)
        {
           
            var org = _dbContext.Organizations.Where(x => x.business_id == requestBusinesID.Id.ToString()).ToList();
            List<OrganizationResponse> orgresponse = new List<OrganizationResponse>();
            foreach (var item in org)
            {
                OrganizationResponse _orgresponse = new OrganizationResponse();
                _orgresponse.Id = item.Id;
                _orgresponse.Organization_type = item.organization_type;
                _orgresponse.Delete = string.Format("<a id='{0}' class='pointer' style='color: blue' onclick ='deleteById(this)'>Delete</a>", item.Id);
                _orgresponse.Edite = string.Format("<a href ='/Category/EditeOrganizationById?id={0}'> Edite </a>", item.Id);
                orgresponse.Add(_orgresponse);
            }

            return Json(orgresponse);
        }
        public ActionResult EditeOrganizationById(int id)
        {
            var selected = _dbContext.Organizations.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.organization = selected;
            return View();
        }
        [HttpPost]
        public JsonResult EditeOrgById([FromBody]RequestModel2 obj)
        {
            var organization = _dbContext.Organizations.Where(x => x.organization_id == obj.orgid && x.business_id == obj.busid).FirstOrDefault();
            organization.organization_type = obj.orgname;
            _dbContext.Organizations.Update(organization);
            _dbContext.SaveChangesAsync();

            return Json("Ok");
        }

            [HttpPost]
        public JsonResult DeleteOrganizationById([FromBody] string id)
        {
            var Selectedorganization = _dbContext.Organizations.Where(x => x.Id == int.Parse(id)).FirstOrDefault();
            _dbContext.Organizations.Remove(Selectedorganization);
            _dbContext.SaveChanges();
            string str = "ok";
            return Json(str);

        }
        public ActionResult InsertBusinessType()
        {

            return View();
        }
        [HttpPost]
        public JsonResult InsertBusinessType([FromBody]string name)
        {
           var lastid= _dbContext.Organizations.Select(x => x.Id).Max();
            var lastbusinessid = _dbContext.Organizations.Where(x=>x.Id==lastid).Select(x => x.business_id).FirstOrDefault();
            Organization org = new Organization() {business_id=(int.Parse(lastbusinessid) +1).ToString(), business_type = name, organization_id = "", organization_type = "" };
            _dbContext.Organizations.Add(org);
            _dbContext.SaveChanges();
            return Json("ok");
        }
        public ActionResult InsertOrganizationType()
        {
            var res = _dbContext.Organizations.Select(x => new KeyValueModel { Id = int.Parse(x.business_id), Name = x.business_type }).Distinct().ToList();
            ViewBag.result = res;
            return View();
        }
        [HttpPost]
        public JsonResult InsertOrganizationType([FromBody]RequestModel requestmodel)
        {
           var res= _dbContext.Organizations.Where(x => x.organization_id == "" && x.business_id == requestmodel.businessid.ToString()).FirstOrDefault();
            var businestype=_dbContext.Organizations.Where(x => x.business_id == requestmodel.businessid.ToString()).Select(x => x.business_type).FirstOrDefault();
            if (res!=null)
            {            
                
                res.organization_id = "1";
                res.organization_type =requestmodel.organizationtype ;
                _dbContext.Organizations.Update(res);
                _dbContext.SaveChanges();
            }
            else
            {
                Organization org = new Organization();
                var lastorgid=int.Parse( _dbContext.Organizations.Where(x => x.business_id == requestmodel.businessid.ToString()).Select(x => x.organization_id).Max());

                org.business_id = requestmodel.businessid.ToString();
                org.organization_id =(lastorgid +1).ToString();
                org.organization_type = requestmodel.organizationtype;
                org.business_type =businestype;
                _dbContext.Organizations.Add(org);
                _dbContext.SaveChanges();

            }

           

            return Json("ok");
        }
        public void GetAllBusiness()
        {
           List<KeyValueModel> lstbusiness = _dbContext.Organizations.Select(x => new KeyValueModel { Id = int.Parse(x.business_id), Name = x.business_type }).Distinct().ToList();
            ViewBag.BusinesList = lstbusiness;
           
        }
    }
    public class RequestModel
    {
        public int businessid { get; set; }
        public string organizationtype { get; set; }
    }
    public class RequestModel2
    {
        public string orgid { get; set; }
        public string orgname { get; set; }
        public string busid { get; set; }

    }

}
