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
    
    public partial class ANSWERS_USERS
    {
        public int ANSWER_USER_ID { get; set; }
        public int USER_ID { get; set; }
        public Nullable<int> ANSWER_ID { get; set; }
        public Nullable<int> IS_CHECKED { get; set; }
        public string USER_ANSWER { get; set; }
        public Nullable<int> QUESTION_ID { get; set; }
        public Nullable<int> SUBJECT_ID { get; set; }
        public Nullable<int> STUDENT_ID { get; set; }
    
        public virtual DOC_ANSWERS DOC_ANSWERS { get; set; }
        public virtual SUBJECTS SUBJECTS { get; set; }
        public virtual USERS USERS { get; set; }
        public virtual DOC_QUESTIONS DOC_QUESTIONS { get; set; }
        public virtual USERS USERS1 { get; set; }
    }
}