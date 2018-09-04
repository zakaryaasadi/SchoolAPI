using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class ChildClass
    {
        public int id { get; set; }
        public string name { get; set; }

        public ChildClass(STUDENTS student)
        {
            if (student == null)
                return;
            id = student.USERS.USER_ID;
            name = student.STUDENT_NAME;
        }

        public ChildClass(int? studentId)
        {
            if (studentId == null)
                return;

            Entities e = new Entities();
            STUDENTS student = e.STUDENTS.FirstOrDefault(s => s.STUDENT_ID == studentId);
            id = student.USERS.USER_ID;
            name = student.STUDENT_NAME;
        }
    }
}