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
    
    public partial class STUDENT_SESSION_DETAILS
    {
        public int STU_SESS_ID { get; set; }
        public int SESSION_ID { get; set; }
        public int STUDENT_ID { get; set; }
        public short EXIST { get; set; }
        public string NOTES { get; set; }
        public short LATE { get; set; }
    
        public virtual SESSIONS SESSIONS { get; set; }
        public virtual STUDENTS STUDENTS { get; set; }
    }
}