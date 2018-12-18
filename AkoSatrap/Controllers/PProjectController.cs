using AkoSatrap.UIHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace AkoSatrap.Controllers
{
    public class PProjectController : Controller
    {
        [HttpGet]
        public JsonResult GetProjectCategoryList()
        {
            int pageNumber = Convert.ToInt32(Request.QueryString["page"]);
            int pageSize = Convert.ToInt32(Request.QueryString["pageSize"]);

            GridResult<ViewModel.ProjectCategoryModel> gridResult = new GridResult<ViewModel.ProjectCategoryModel>();
            var allCategory = new Business.ProjectBusiness().GetAllProjectCategory();

            gridResult.Data = allCategory.OrderByDescending(r => r.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            gridResult.Total = allCategory.Count;

            return Json(gridResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProjectImages(string imageFolderName)
        {
            ViewModel.ReturnResult<List<string>> returnResult = new ViewModel.ReturnResult<List<string>>();
            var path = Server.MapPath($"~/AkoSatrapImages/{imageFolderName}/");
            if (Directory.Exists(path))
            {
                returnResult.Data = Directory.GetFiles(path).Select(r => Path.GetFileName(r)).ToList();
            }
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult GetProjectListForDropDown()
        {
            var allProduct = new List<ProjectModel>();
            allProduct.Add(new ProjectModel
            {
                Title = "",
                Id = 0
            });
            allProduct.AddRange(new Business.ProjectBusiness().GetAllProject(false));
            return Json(allProduct, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddProjectFeature(ViewModel.ProjectFeatureModel projectFeature)
        {
            var business = new Business.ProjectBusiness();
            var returnResult = business.AddProjectDetail(projectFeature);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult UpdateProjectDetail(ViewModel.ProjectFeatureModel projectFeature)
        {
            var business = new Business.ProjectBusiness();
            var returnResult = business.UpdateProjectFeature(projectFeature);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult GetProjectDetail(int projectId)
        {
            var gridResult = new GridResult<ViewModel.ProjectFeatureModel>();
            var allDetail = new Business.ProjectBusiness().GetAllProjectDetail(projectId);

            gridResult.Data = allDetail.OrderByDescending(r => r.Id).ToList();
            gridResult.Total = allDetail.Count;

            return Json(gridResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProjectList()
        {
            var gridResult = new GridResult<ViewModel.ProjectModel>();
            var allProjects = new Business.ProjectBusiness().GetAllProject(false);

            gridResult.Data = allProjects.OrderByDescending(r => r.Id).ToList();
            gridResult.Total = allProjects.Count;

            return Json(gridResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddImage(FileAttachment fileAttachment, int id)
        {
            ViewModel.ReturnResult<bool> returnResult = new ViewModel.ReturnResult<bool>();
            var business = new Business.ProjectBusiness();
            var project = business.GetProjectById(id);
            var path = Server.MapPath($"~/AkoSatrapImages/{project.ImageFolderName}/");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            fileAttachment.Attachment.SaveAs($"{path}/{Guid.NewGuid()}.{fileAttachment.Attachment.FileName.Split('.')[1].ToString()}");

            return Json(returnResult);

        }

        [HttpPost]
        public JsonResult AddProject(ViewModel.ProjectModel project)
        {
            var business = new Business.ProjectBusiness();
            var returnResult = business.AddProject(project);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult AddProjectCategory(ViewModel.ProjectCategoryModel model)
        {
            var business = new Business.ProjectBusiness();
            var returnResult = business.AddProjectCategory(model);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult UpdateProjectCategory(ViewModel.ProjectCategoryModel model)
        {
            var business = new Business.ProjectBusiness();
            var returnResult = business.UpdateProjectCategory(model);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult UpdateProject(ViewModel.ProjectModel project)
        {
            var business = new Business.ProjectBusiness();
            var returnResult = business.UpdateProject(project);
            return Json(returnResult);
        }

        [HttpPost]
        public JsonResult GetProjectCategoryListForDropDown()
        {
            var allCategory = new Business.ProjectBusiness().GetAllProjectCategory();
            return Json(allCategory, JsonRequestBehavior.AllowGet);
        }
    }
}