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
    
    public partial class TEACHER_SUBJECTS
    {
        public int TEACHER_SUBJECT_ID { get; set; }
        public Nullable<int> TEACHER_ID { get; set; }
        public Nullable<int> SUBJECT_ID { get; set; }
    
        public virtual SUBJECTS SUBJECTS { get; set; }
        public virtual TEACHERS TEACHERS { get; set; }
    }
}
