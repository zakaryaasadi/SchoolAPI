//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolAPI.Models.MD
{
    using System;
    using System.Collections.Generic;
    
    public partial class COURSES
    {
        public int COURSE_ID { get; set; }
        public int DAY_OF_THE_WEEK { get; set; }
        public int ORDER_IN_WEEK { get; set; }
        public Nullable<int> TEACHER_ID { get; set; }
        public int CLASS_ID { get; set; }
        public short IS_MANUALLY { get; set; }
        public Nullable<int> SUBJECT_ID { get; set; }
    
        public virtual CLASSES CLASSES { get; set; }
        public virtual SUBJECTS SUBJECTS { get; set; }
        public virtual TEACHERS TEACHERS { get; set; }
    }
}