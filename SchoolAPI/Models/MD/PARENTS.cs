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
    
    public partial class PARENTS
    {
        public PARENTS()
        {
            this.STUDENTS_PARENTS = new HashSet<STUDENTS_PARENTS>();
        }
    
        public int PARENT_ID { get; set; }
        public Nullable<int> SCHOOL_ID { get; set; }
        public Nullable<int> USER_ID { get; set; }
        public string PARENT_MOTHER { get; set; }
        public string PARENT_MOTHER_MPHONE { get; set; }
        public string PARENT_MOTHER_LPHONE { get; set; }
        public string PARENT_FATHER_NAME { get; set; }
        public string PARENT_FATHER_MPHONE { get; set; }
        public string PARENT_FATHER_LPHONE { get; set; }
        public Nullable<short> PARENT_ACTIVE { get; set; }
        public string PARENT_ADRESS { get; set; }
        public string PARENT_AREA { get; set; }
        public string PARENT_MOTHER_EMAIL { get; set; }
        public string PARENT_FATHER_EMAIL { get; set; }
        public Nullable<int> MAIN_ID { get; set; }
    
        public virtual SCHOOLS SCHOOLS { get; set; }
        public virtual USERS USERS { get; set; }
        public virtual ICollection<STUDENTS_PARENTS> STUDENTS_PARENTS { get; set; }
    }
}
