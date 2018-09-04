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
    
    public partial class SUBJECTS
    {
        public SUBJECTS()
        {
            this.NEWS_ACL_SUBJECTS = new HashSet<NEWS_ACL_SUBJECTS>();
            this.STUDENT_SUBJECTS = new HashSet<STUDENT_SUBJECTS>();
            this.TEACHER_SUBJECTS = new HashSet<TEACHER_SUBJECTS>();
            this.MESSAGES = new HashSet<MESSAGES>();
        }
    
        public int SUBJECT_ID { get; set; }
        public Nullable<int> SCHOOL_ID { get; set; }
        public Nullable<int> CLASS_ID { get; set; }
        public string SUBJECT_NAME { get; set; }
        public Nullable<int> SUBJECT_MAX { get; set; }
        public Nullable<int> SUBJECT_PASS { get; set; }
        public Nullable<int> SORT { get; set; }
        public Nullable<System.DateTime> EXPIRE_DATE { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public Nullable<int> MAIN_ID { get; set; }
    
        public virtual CLASSES CLASSES { get; set; }
        public virtual ICollection<NEWS_ACL_SUBJECTS> NEWS_ACL_SUBJECTS { get; set; }
        public virtual SCHOOLS SCHOOLS { get; set; }
        public virtual ICollection<STUDENT_SUBJECTS> STUDENT_SUBJECTS { get; set; }
        public virtual ICollection<TEACHER_SUBJECTS> TEACHER_SUBJECTS { get; set; }
        public virtual ICollection<MESSAGES> MESSAGES { get; set; }
    }
}
