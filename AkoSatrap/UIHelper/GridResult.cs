using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkoSatrap.UIHelper
{
    public class GridResult<T>
    {
        public int Total { get; set; }
        public List<T> Data { get; set; }
    }
}