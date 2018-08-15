using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class UserClass
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
        public byte[] profileImage { get; set; }
        public ParentClass parents { get; set; }
        public UserTypeClass type { get; set; }
        public List<GroupClass> groups { get; set; }
        public List<ClassClass> classes { get; set; }
        public List<SubjectClass> subjects { get; set; }
        public List<ChildClass> children { get; set; }
    }
}