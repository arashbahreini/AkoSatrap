using System;
using System.Collections.Generic;
using System.Globalization;
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
                enDbProject.StartDate = ConvertPersianDateToGregorian(project.StartDate);
                enDbProject.EndDate = ConvertPersianDateToGregorian(project.EndDate);
                enDbProject.CompletionPercentage = project.CompletionPercentage;
                enDbProject.ProjectCategoryId = project.ProjectCategory != null? project.ProjectCategory.EnId : 0;
                enDbProject.IsEnglish = true;
                enDbProject.ImageFolderName = Guid.NewGuid().ToString();

                dbProject.Title = project.Title;
                dbProject.Description = project.Description;
                dbProject.Province = project.Province;
                dbProject.City = project.City;
                dbProject.CreateDate = DateTime.Now;
                dbProject.StartDate =ConvertPersianDateToGregorian(project.StartDate);
                dbProject.EndDate = ConvertPersianDateToGregorian(project.EndDate);
                dbProject.CompletionPercentage = project.CompletionPercentage;
                dbProject.ProjectCategoryId = project.ProjectCategory != null ? project.ProjectCategory.Id : 0;
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

        public DateTime? ConvertPersianDateToGregorian(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            var result = new DateTime(
                int.Parse(input.Substring(0, 4)),
                int.Parse(input.Substring(4, 2)),
                int.Parse(input.Substring(6, 2)),
                new PersianCalendar());

            return result;
        }

        public static string ConvertGregorianToPersianDate(DateTime? input)
        {
            if (!input.HasValue)
            {
                return "";
            };
            var month = new PersianCalendar().GetMonth(input.Value).ToString().Length == 1 ? 0 + new PersianCalendar().GetMonth(input.Value).ToString() : new PersianCalendar().GetMonth(input.Value).ToString();
            var day = new PersianCalendar().GetDayOfMonth(input.Value).ToString().Length == 1 ? 0 + new PersianCalendar().GetDayOfMonth(input.Value).ToString() : new PersianCalendar().GetDayOfMonth(input.Value).ToString();
            var result = new PersianCalendar().GetYear(input.Value) + "/" + month + "/" + day;
            return result;
        }

        public List<ViewModel.ProjectModel> GetAllProject(bool isEnglish)
        {
            var context = new DomainDeriven.AkoSatrapDb();

            var projects = context.Projects.Include("Project2").Include("ProjectCategory").Where(r => r.IsEnglish == isEnglish)
                .ToList()
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
                    StartDate = ConvertGregorianToPersianDate(r.StartDate),
                    Province = r.Province,
                    master = r.master,
                    ImageFolderName = r.ImageFolderName,
                    Id = r.Id,
                    Description = r.Description,
                    CreateDate  = r.CreateDate,
                    CompletionPercentage = r.CompletionPercentage,
                    City = r.City,
                    EndDate = ConvertGregorianToPersianDate(r.EndDate),
                    EnId = r.EnId,
                    EnCity = r.Project2.City,
                    EnDescription = r.Project2.Description,
                    EnProvince = r.Project2.Province,
                    EnTitle = r.Project2.Title,
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

        public ViewModel.ProjectModel GetProjectById(int id)
        {
            var context = new DomainDeriven.AkoSatrapDb();
            var project = context.Projects.FirstOrDefault(r => r.Id == id);
            var projectViewModel = new ViewModel.ProjectModel
            {
                Id = project.Id,
                ProjectCategoryId = project.ProjectCategoryId.Value,
                Description = project.Description,
                ImageFolderName = project.ImageFolderName
            };
            return projectViewModel;
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
