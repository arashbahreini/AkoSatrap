using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class ProjectBusiness
    {
        public List<ViewModel.ProjectCategory> GetAllProductCategory()
        {
            DomainDeriven.AkoSatrapDb context = new DomainDeriven.AkoSatrapDb();
            var result = context.ProjectCategories.Include("ProjectCategory2").Where(r => r.IsEnglish == false)
                .Select(r => new ViewModel.ProjectCategory{ Title = r.Title, EnTitle = r.ProjectCategory2.Title, Id = r.Id, EnId = r.EnId.Value }).ToList();
            return result;
        }

        public ViewModel.ReturnResult<bool> AddProjectCategory(ViewModel.ProjectCategory projectCategory)
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

        public ViewModel.ReturnResult<bool> UpdateProjectCategory(ViewModel.ProjectCategory model)
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
