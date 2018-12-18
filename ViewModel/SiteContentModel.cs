using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class SiteContentModel
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
