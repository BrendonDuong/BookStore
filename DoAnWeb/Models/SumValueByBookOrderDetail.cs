using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    public class SumValueByBookOrderDetail
    {
        public int BookId { get; set; }
        public int SumQuantity { get; set; }
        public decimal SumPrice { get; set; }
    }
}