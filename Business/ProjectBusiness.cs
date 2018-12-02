using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Business
{
    public class ProjectBusiness
    {
        public List<ViewModel.ProjectCategoryModel> GetAllProjectCategory()
        {
            DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();
            var result = context.ProjectCategories.Include("ProjectCategory2").Where(r => r.IsEnglish == false)
                .Select(r => new ViewModel.ProjectCategoryModel{ Title = r.Title, EnTitle = r.ProjectCategory2.Title, Id = r.Id, EnId = r.EnId.Value }).ToList();
            return result;
        }

        public ViewModel.ReturnResult<bool> AddProject(ViewModel.ProjectModel project)
        {
            ViewModel.ReturnResult<bool> returnResult = new ViewModel.ReturnResult<bool>();

            try
            {
                DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();
                var enDbProject = new DomainDeriven.Project();
                var dbProject = new DomainDeriven.Project();

                enDbProject.Title = project.EnTitle;
                enDbProject.Description = project.EnDescription;
                enDbProject.Province = project.EnProvince;
                enDbProject.City = project.EnCity;
                enDbProject.CreateDate = DateTime.Now;
                enDbProject.StartDate = project.StartDate;
                enDbProject.EndDate = project.EndDate;
                enDbProject.CompletionPercentage = project.CompletionPercentage;
                enDbProject.ProjectCategoryId = project.ProjectCategory.EnId;
                enDbProject.IsEnglish = true;
                enDbProject.ImageFolderName = Guid.NewGuid().ToString();

                dbProject.Title = project.Title;
                dbProject.Description = project.Description;
                dbProject.Province = project.Province;
                dbProject.City = project.City;
                dbProject.CreateDate = DateTime.Now;
                dbProject.StartDate = project.StartDate;
                dbProject.EndDate = project.EndDate;
                dbProject.CompletionPercentage = project.CompletionPercentage;
                dbProject.ProjectCategoryId = project.ProjectCategory.Id;
                dbProject.IsEnglish = false;
                dbProject.ImageFolderName = Guid.NewGuid().ToString();
                dbProject.Project2 = enDbProject;

                context.Projects.Add(dbProject);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                returnResult.SetError(ex.Message);
            }

            return returnResult;
        }

        public List<ViewModel.ProjectModel> GetAllProject()
        {
            var context = new DomainDeriven.AkoSatrapDb();

            var projects = context.Projects.Include("Project2").Include("ProjectCategory").Where(r => r.IsEnglish == false)
                .Select(r => new ViewModel.ProjectModel
                {
                    ProjectCategory = new ViewModel.ProjectCategoryModel
                    {
                        Title = r.ProjectCategory.Title,
                        Id = r.ProjectCategory.Id,
                        EnId = r.Project2.ProjectCategoryId.Value,
                        EnTitle = r.Project2.ProjectCategory.Title
                    },
                    IsEnglish = r.IsEnglish,
                    ProjectCategoryId = r.ProjectCategoryId,
                    Title = r.Title,
                    StartDate = r.StartDate,
                    Province = r.Province,
                    master = r.master,
                    ImageFolderName = r.ImageFolderName,
                    Id = r.Id,
                    Description = r.Description,
                    CreateDate  = r.CreateDate,
                    CompletionPercentage = r.CompletionPercentage,
                    City = r.City,
                    EndDate = r.EndDate,
                    EnId = r.EnId,
                }).ToList();

            projects.ForEach(item =>
            {
                var path = System.Web.Hosting.HostingEnvironment.MapPath($"~/AkoSatrapImages/{item.ImageFolderName}/");
                if (Directory.Exists(path))
                {
                    item.Images = Directory.GetFiles(path).Select(r => Path.GetFileName(r)).ToList();
                }
            });
            return projects;
        }

        public ViewModel.ReturnResult<bool> AddProjectCategory(ViewModel.ProjectCategoryModel projectCategory)
        {
            var returnResult = new ViewModel.ReturnResult<bool>();
            try
            {
                var context = new DomainDeriven.AkoSatrapDb();
                var enDbProjectCategory = new DomainDeriven.ProjectCategory();
                var dbProjectCategory = new DomainDeriven.ProjectCategory();

                enDbProjectCategory.Title = projectCategory.EnTitle;
                enDbProjectCategory.IsEnglish = true;

                dbProjectCategory.Title = projectCategory.Title;
                dbProjectCategory.IsEnglish = false;
                dbProjectCategory.ProjectCategory2 = enDbProjectCategory;
                context.ProjectCategories.Add(dbProjectCategory);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                returnResult.SetError(ex.Message);
            }

            return returnResult;
        }

        public ViewModel.ReturnResult<bool> UpdateProjectCategory(ViewModel.ProjectCategoryModel model)
        {
            var returnResult = new ViewModel.ReturnResult<bool>();
            try
            {
                var context = new DomainDeriven.AkoSatrapDb();

                var dbProjectCategory = context.ProjectCategories.Include("ProjectCategory2").FirstOrDefault(r => r.IsEnglish == false && r.Id == model.Id);
                if (dbProjectCategory != null)
                {
                    dbProjectCategory.Title = model.Title;
                    dbProjectCategory.ProjectCategory2.Title = model.EnTitle;
                    context.SaveChanges();
                }
                else
                {
                    returnResult.SetError("دسته بندی مورد نظر پیدا نشد");
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                returnResult.SetError(ex.Message);
            }
            return returnResult;
        }
    }
}
