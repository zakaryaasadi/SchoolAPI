using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class NewsClass
    {
        public int id { get; set; }
        public string personName { get; set; }

        public int userId { get; set; }
        public string userName { get; set; }
        public byte[] profileImage { get; set; }
        public short? type { get; set; }

        public string title { get; set; }
        public Nullable<DateTime> creationDate { get; set; }
        public string categoryName { get; set; }
        public SubcategoryClass subcategory { get; set; }
        public string headLine { get; set; }
        public string body { get; set; }
        public byte[] newsImage { get; set; }
        public bool sharable { get; set; }
        public Nullable<DateTime> eventDate { get; set; }

    }
}