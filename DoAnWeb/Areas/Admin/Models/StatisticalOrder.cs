using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Models
{
    public class StatisticalOrder
    {
        public int OrderId { get; set; }
        public Book Book { get; set; }
        public int MyProperty { get; set; }
    }
}