using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDeriven
{
    [Table("SiteContents")]
    public class SiteContent
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
