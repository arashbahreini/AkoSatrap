using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ProductFeature
    {
        public int Id { get; set; }

        public int EnId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string EnTitle { get; set; }

        public string EnDescription { get; set; }

        public byte Order { get; set; }
        public int ProductId { get; set; }
    }
}
