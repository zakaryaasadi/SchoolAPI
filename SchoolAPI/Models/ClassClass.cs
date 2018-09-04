using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class ClassClass
    {
        public int? id { get; set; }
        public string name { get; set; }

        public ClassClass(CLASSES _class)
        {
            if (_class == null)
                return;

            id = _class.CLASS_ID;
            name = _class.CLASS_NAME;
        }
    }
}