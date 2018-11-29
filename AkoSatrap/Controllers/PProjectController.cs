using AkoSatrap.UIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkoSatrap.Controllers
{
    public class PProjectController : Controller
    {
        [HttpGet]
        public JsonResult GetProjectCategoryList()
        {
            int pageNumber = Convert.ToInt32(Request.QueryString["page"]);
            int pageSize = Convert.ToInt32(Request.QueryString["pageSize"]);

            GridResult<ViewModel.ProjectCategory> gridResult = new GridResult<ViewModel.ProjectCategory>();
            var allCategory = new Business.ProjectBusiness().GetAllProductCategory();

            gridResult.Data = allCategory.OrderByDescending(r => r.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            gridResult.Total = allCategory.Count;

            return Json(gridResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddProjectCategory(ViewModel.ProjectCategory model)
        {
            var business = new Business.ProjectBusiness();
            var returnResult = business.AddProjectCategory(model);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult UpdateProjectCategory(ViewModel.ProjectCategory model)
        {
            var business = new Business.ProjectBusiness();
            var returnResult = business.UpdateProjectCategory(model);
            return Json(returnResult);
        }
    }
}