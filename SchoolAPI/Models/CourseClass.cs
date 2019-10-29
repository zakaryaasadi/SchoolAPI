using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class CourseClass
    {
        public int id { get; set; }
        public SubjectClass subject { get; set; }
        public UserPublicInfoClass teacher { get; set; }
        public ClassClass _class { get; set; }
        public int order_in_week { get; set; }
        public string day { get; set; }

        public CourseClass(COURSES co)
        {
            id = co.COURSE_ID;
            subject = new SubjectClass(co.SUBJECT_ID);
            if(co.IS_MANUALLY == 0)
                 teacher = new UserPublicInfoClass(co.TEACHERS.USERS);
            _class = new ClassClass(co.CLASSES);
            order_in_week = co.ORDER_IN_WEEK;
            Day d = (Day) co.DAY_OF_THE_WEEK - 1;
            day = d.ToString();

        }

    }


    enum Day
    {
        Sunday, Monday, Tuesday, Wednesday,Thuesday,Friday,Saturday
    }
}