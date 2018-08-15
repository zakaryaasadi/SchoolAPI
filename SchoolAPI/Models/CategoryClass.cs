using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class CategoryClass
    {
        public int id { get; set; }
        public string title { get; set; }
        public byte[] image { get; set; }
        public List<SubcategoryClass> subcategories { get; set; }
    }
}