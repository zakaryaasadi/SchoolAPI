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
    
    public partial class STUDENTS_NOTES
    {
        public int STUDENT_NOTE_ID { get; set; }
        public Nullable<int> STUDENT_ID { get; set; }
        public string NOTE { get; set; }
        public Nullable<System.DateTime> NOTE_DATE { get; set; }
        public Nullable<int> USER_ID { get; set; }
    
        public virtual STUDENTS STUDENTS { get; set; }
        public virtual USERS USERS { get; set; }
    }
}