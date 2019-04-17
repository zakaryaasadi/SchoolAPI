﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class Result
    {
        public int statusCode { get; set; }
        public int totalPage { get; set; }
        public int numResult { get; set; }
        public int page { get; set; }
        public string status { get; set; }
        public object results { get; set; }
    }
}