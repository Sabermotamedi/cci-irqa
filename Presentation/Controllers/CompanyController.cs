using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Presentation.Data;
using Presentation.Models;
using Presentation.Models.RequestModel;
using Presentation.Models.ResponseModels;
using Presentation.ResponseModels;

namespace Presentation.Controllers
{
    public class CompanyController : Controller
    {
        ApplicationDbContext _dbContext;

        public CompanyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult SearchByBusiness()
        {

            var res = _dbContext.Organizations.Select(x => new KeyValueModel { Id = int.Parse(x.business_id), Name = x.business_type }).Distinct().ToList();
            ViewBag.result = res;


            return View();
        }
        [HttpGet]
        public ActionResult SearchByName()
        {
            var res = _dbContext.Organizations.Select(x => new KeyValueModel { Id = int.Parse(x.business_id), Name = x.business_type }).Distinct().ToList();
            ViewBag.result = res;


            return View();
        }
        [HttpPost]
        public JsonResult GetCompanyByName([FromBody]GetCompanyByName getCompanyByName)
        {
            List<Company> lstcompany = new List<Company>();
            List<Company> lstcompany2 = new List<Company>();
            lstcompany = _dbContext.Companies.Where(x => x.Business_Id == getCompanyByName.Id.ToString()).ToList();
            lstcompany2 = lstcompany.Where(x=> x.Organization_Name_Ar.Contains(getCompanyByName.Name) || x.Organization_Name_En.Contains(getCompanyByName.Name)).ToList();
            List<CompanyResponse> lstcompanyresponse = new List<CompanyResponse>();
            foreach (var item in lstcompany2)
            {
                CompanyResponse CopmanyResponse = new CompanyResponse();
                CopmanyResponse.Address_Ar = item.Address_Ar;
                CopmanyResponse.Address_En = item.Address_En;
                CopmanyResponse.Area = item.Area;
                CopmanyResponse.Organization_Id = item.Organization_Id;
                CopmanyResponse.City_Name_Ar = item.City_Name_Ar;
                CopmanyResponse.City_Name_En = item.City_Name_En;
                CopmanyResponse.Company_Type = item.Company_Type;
                CopmanyResponse.Contact_Person_Ar = item.Contact_Person_Ar;
                CopmanyResponse.Contact_Person_En = item.Contact_Person_En;
                CopmanyResponse.Cr_Number = item.Cr_Number;
                CopmanyResponse.Email = item.Email;
                CopmanyResponse.Fax_No = item.Fax_No;
                CopmanyResponse.Id = item.Id;
                CopmanyResponse.Membership_Date = item.Membership_Date;
                CopmanyResponse.Membership_Next_RenewDdate = item.Membership_Next_RenewDdate;
                CopmanyResponse.Membership_Renew_Date = item.Membership_Next_RenewDdate;
                CopmanyResponse.Organization_Name_Ar = item.Organization_Name_Ar;
                CopmanyResponse.Organization_Name_En = item.Organization_Name_En;
                CopmanyResponse.Business_Id = item.Business_Id;
                CopmanyResponse.Phone_No = item.Phone_No;
                CopmanyResponse.Po_Box = item.Po_Box;
                CopmanyResponse.Website = item.Website;
                CopmanyResponse.Delete = string.Format("<a id='{0}' style='color: blue' class='pointer' onclick='deleteById(this)'>Delete</a>", item.Id);

                CopmanyResponse.Edite = string.Format("<a href ='/Company/Edite?id={0}'> Edite </a>", item.Id);
                lstcompanyresponse.Add(CopmanyResponse);

            }

            return Json(lstcompanyresponse);
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
            company = _dbContext.Companies.Where(x => x.Business_Id == requestOrgBisId.businessid.ToString()
            && x.Organization_Id == requestOrgBisId.organizationid.ToString()).ToList();
            List<CompanyResponse> LstCompoanyResponse = new List<CompanyResponse>();

            foreach (var item in company)
            {
                CompanyResponse CopmanyResponse = new CompanyResponse();
                CopmanyResponse.Address_Ar = item.Address_Ar;
                CopmanyResponse.Address_En = item.Address_En;
                CopmanyResponse.Area = item.Area;
                CopmanyResponse.Organization_Id = item.Organization_Id;
                CopmanyResponse.City_Name_Ar = item.City_Name_Ar;
                CopmanyResponse.City_Name_En = item.City_Name_En;
                CopmanyResponse.Company_Type = item.Company_Type;
                CopmanyResponse.Contact_Person_Ar = item.Contact_Person_Ar;
                CopmanyResponse.Contact_Person_En = item.Contact_Person_En;
                CopmanyResponse.Cr_Number = item.Cr_Number;
                CopmanyResponse.Email = item.Email;
                CopmanyResponse.Fax_No = item.Fax_No;
                CopmanyResponse.Id = item.Id;
                CopmanyResponse.Membership_Date = item.Membership_Date;
                CopmanyResponse.Membership_Next_RenewDdate = item.Membership_Next_RenewDdate;
                CopmanyResponse.Membership_Renew_Date = item.Membership_Renew_Date;
                CopmanyResponse.Membership_Renew_Date = item.Membership_Renew_Date;
                CopmanyResponse.Organization_Name_Ar = item.Organization_Name_Ar;
                CopmanyResponse.Organization_Name_En = item.Organization_Name_En;
                CopmanyResponse.Business_Id = item.Business_Id;
                CopmanyResponse.Phone_No = item.Phone_No;
                CopmanyResponse.Po_Box = item.Po_Box;
                CopmanyResponse.Website = item.Website;
                CopmanyResponse.Delete = string.Format("<a id='{0}' class='pointer' style='color: blue' onclick ='deleteById(this)'>Delete</a>", item.Id);
                CopmanyResponse.Edite = string.Format("<a href ='/Company/Edite?id={0}'> Edite </a>", item.Id);

                LstCompoanyResponse.Add(CopmanyResponse);

            }

            return Json(LstCompoanyResponse);

        }
        [HttpPost]
        public JsonResult DeleteById([FromBody] string id)
        {

            var Selectedcompany = _dbContext.Companies.Where(x => x.Id == int.Parse(id)).FirstOrDefault();
            _dbContext.Companies.Remove(Selectedcompany);
            _dbContext.SaveChanges();

            string str = "ok";
            return Json(str);
        }
        [HttpGet]
        public ActionResult Edite(int id)
        {

            KeyValueModel SelectedBusinesType = new KeyValueModel();
            KeyValueModel1 SelectedOrganizationType = new KeyValueModel1();

            var CompanyModel = _dbContext.Companies.Where(x => x.Id == id).FirstOrDefault();

            var SelectedOrganization = _dbContext.Organizations.Where(x => x.organization_id == CompanyModel.Organization_Id && x.business_id == CompanyModel.Business_Id).FirstOrDefault();

            SelectedBusinesType.Id = int.Parse(SelectedOrganization.business_id);
            SelectedBusinesType.Name = SelectedOrganization.business_type;

            SelectedOrganizationType.organizationId = SelectedOrganization.organization_id;
            SelectedOrganizationType.organizationtype = SelectedOrganization.organization_type;





            List<KeyValueModel> BusinesList = _dbContext.Organizations.Select(x => new KeyValueModel { Id = int.Parse(x.business_id), Name = x.business_type }).Distinct().ToList();

            List<KeyValueModel1> lstorganization = _dbContext.Organizations.Where(x => x.business_id == CompanyModel.Business_Id).Select(x => new KeyValueModel1 { organizationId = x.organization_id, organizationtype = x.organization_type }).ToList();



            ViewBag.selected = CompanyModel;
            ViewBag.BusinesList = BusinesList;

            ViewBag.organizationList = lstorganization;

            ViewBag.BusinesType = convertBusinesListToHtmlSelectOption(BusinesList, SelectedBusinesType);
            ViewBag.Organizationtype = convertOrganizationListToHtmlSelectOption(lstorganization, SelectedOrganizationType);



            return View();
        }
        public string convertBusinesListToHtmlSelectOption(List<KeyValueModel> BusinesList, KeyValueModel SelectedBusinesType)
        {
            string str = "";

            foreach (var item in BusinesList)
            {
                string selected = string.Empty;

                if (item.Id == SelectedBusinesType.Id)
                {
                    selected = "selected";
                }

                str += string.Format("<Option Value={0} {2}>{1}</Option>", item.Id, item.Name, selected);
            }
            return str;
        }
        public string convertOrganizationListToHtmlSelectOption(List<KeyValueModel1> lstorganization, KeyValueModel1 SelectedOrganizationType)
        {
            string str = "";

            foreach (var item in lstorganization)
            {
                string selected = string.Empty;

                if (item.organizationId == SelectedOrganizationType.organizationId)
                {
                    selected = "selected";
                }

                str += string.Format("<Option Value={0} {2}>{1}</Option>", item.organizationId, item.organizationtype, selected);
            }
            return str;
        }
        [HttpPost]
        public JsonResult EditeById([FromBody] Company company)
        {
            _dbContext.Companies.Update(company);
            _dbContext.SaveChanges();
            string str = "ok";
            return Json(str);
        }
        public ActionResult Insert()
        {
            List<KeyValueModel> BusinesList = _dbContext.Organizations.Select(x => new KeyValueModel { Id = int.Parse(x.business_id), Name = x.business_type }).Distinct().ToList();
            ViewBag.BusinesList = BusinesList;
            return View();
        }
        [HttpPost]
        public JsonResult InsertOnDb([FromBody] CompanyRequest company)
        {
            Company _company = new Company();
            _company.Address_Ar = company.Address_Ar;
            _company.Address_En = company.Address_En;
            _company.Area = company.Area;
            _company.Organization_Id = company.Organization_Id;
            _company.City_Name_Ar = company.City_Name_Ar;
            _company.City_Name_En = company.City_Name_En;
            _company.Company_Type = company.Company_Type;
            _company.Contact_Person_Ar = company.Contact_Person_Ar;
            _company.Contact_Person_En = company.Contact_Person_En;
            _company.Cr_Number = company.Cr_Number;
            _company.Email = company.Email;
            _company.Fax_No = company.Fax_No;
            _company.Membership_Date = company.Membership_Date;
            _company.Membership_Next_RenewDdate = company.Membership_Next_RenewDdate;
            _company.Membership_Renew_Date = company.Membership_Renew_Date;
            _company.Organization_Name_Ar = company.Organization_Name_Ar;
            _company.Organization_Name_En = company.Organization_Name_En;
            _company.Business_Id = company.Business_Id;
            _company.Phone_No = company.Phone_No;
            _company.Po_Box = company.Po_Box;
            _company.Website = company.Website;

            _dbContext.Companies.Add(_company);
            _dbContext.SaveChangesAsync();
            string str = "ok";
            return Json(str);
        }
       
        
       
      
      
        

    }
    public class GetCompanyByName
    {
        public int Id { get; set; }
        public string Name { get; set; }

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