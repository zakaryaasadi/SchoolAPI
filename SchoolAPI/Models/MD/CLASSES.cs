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
    
    public partial class CLASSES
    {
        public CLASSES()
        {
            this.NEWS_ACL_CLASSES = new HashSet<NEWS_ACL_CLASSES>();
            this.STUDENTS = new HashSet<STUDENTS>();
            this.SUBJECTS = new HashSet<SUBJECTS>();
            this.MESSAGES = new HashSet<MESSAGES>();
        }
    
        public int CLASS_ID { get; set; }
        public Nullable<int> SCHOOL_ID { get; set; }
        public Nullable<int> NEWS_CAT_ID { get; set; }
        public string CLASS_NAME { get; set; }
        public Nullable<int> SORT { get; set; }
        public Nullable<int> MAIN_ID { get; set; }
    
        public virtual NEWS_CATS NEWS_CATS { get; set; }
        public virtual SCHOOLS SCHOOLS { get; set; }
        public virtual ICollection<NEWS_ACL_CLASSES> NEWS_ACL_CLASSES { get; set; }
        public virtual ICollection<STUDENTS> STUDENTS { get; set; }
        public virtual ICollection<SUBJECTS> SUBJECTS { get; set; }
        public virtual ICollection<MESSAGES> MESSAGES { get; set; }
    }
}
