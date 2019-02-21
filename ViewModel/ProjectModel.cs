using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ProjectModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectModel()
        {
            Project1 = new HashSet<ProjectModel>();
            ProjectFeatures = new HashSet<ProjectFeatureModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public bool? IsEnglish { get; set; }

        public int? EnId { get; set; }

        public DateTime? CreateDate { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public double? CompletionPercentage { get; set; }

        public string master { get; set; }

        public int? ProjectCategoryId { get; set; }

        public string ImageFolderName { get; set; }

        public virtual ICollection<ProjectModel> Project1 { get; set; }

        public virtual ProjectModel Project2 { get; set; }

        public virtual ProjectCategoryModel ProjectCategory { get; set; }

        public virtual ICollection<ProjectFeatureModel> ProjectFeatures { get; set; }
        public string EnTitle { get; set; }
        //public string EndDescription { get; set; }
        public string EnProvince { get; set; }
        public string EnDescription { get; set; }
        public string EnCity { get; set; }
        public List<string> Images { get; set; }
        public string GridStartDate { get; set; }
        public string GridEndDate { get; set; }
        public string GenerateSlug()
        {
            string result = Title;
            result = result.Replace(' ', '-');

            return result;
        }
    }
}
