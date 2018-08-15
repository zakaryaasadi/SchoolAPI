using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class Result
    {
        public int statusCode { get; set; }
        public int total_page { get; set; }
        public int num_result { get; set; }
        public int page { get; set; }
        public string status { get; set; }
        public object results { get; set; }
    }
}