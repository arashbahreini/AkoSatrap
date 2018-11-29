using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ViewModel
{
    public class Product
    {
        public int Id { get; set; }
        public int EnId { get; set; }
        public int CategoryId { get; set; }
        public ProductCategoty Category { get; set; }

        public List<ProductFeature> ProductFeature { get; set; }
        public string Brand { get; set; }
        public string EnBrand { get; set; }
        public string Title { get; set; }
        public string EnTitle { get; set; }
        public string ImageFolderName { get; set; }
        public string ImportantTip1 { get; set; }
        public string EnImportantTip1 { get; set; }
        public string ImportantTip2 { get; set; }
        public string EnImportantTip2 { get; set; }
        public string ImportantTip3 { get; set; }
        public string EnImportantTip3 { get; set; }

        public string Description { get; set; }
        public string EnDescription { get; set; }

        public List<string> Images { get; set; }
        public string GenerateSlug()
        {
            string result = Title;
            result = result.Replace(' ', '-');

            return result;
        }

        
    }
}
