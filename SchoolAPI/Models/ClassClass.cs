﻿using SchoolAPI.Models.MD;
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
        public List<SubjectClass> subjects = new List<SubjectClass>();
        public ClassClass(CLASSES _class)
        {
            if (_class == null)
                return;

            id = _class.CLASS_ID;
            name = _class.CLASS_NAME;
            foreach (var sub in _class.SUBJECTS)
                subjects.Add(new SubjectClass(sub));
        }

        public ClassClass(CLASSES _class, List<SUBJECTS> list)
        {
            if (_class == null)
                return;

            id = _class.CLASS_ID;
            name = _class.CLASS_NAME;
            foreach (var sub in list)
                subjects.Add(new SubjectClass(sub));
        }
    
    }
}