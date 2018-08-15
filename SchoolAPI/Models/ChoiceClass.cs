using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class ChoiceClass
    {
        public int id { get; set; }
        public string title { get; set; }
        public int voteCount { get; set; }
        public bool isChoiced { get; set; }
        public int newsId { get; set; }
    }
}