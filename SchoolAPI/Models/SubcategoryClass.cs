using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class SubcategoryClass
    {
        public int id { get; set; }
        public string title { get; set; }
        public int categoryId { get; set; }
    }
}