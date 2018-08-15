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
    
    public partial class NEWS_CATS
    {
        public NEWS_CATS()
        {
            this.ADMINS = new HashSet<ADMINS>();
            this.CLASSES = new HashSet<CLASSES>();
            this.NEWS_SUB_CATS = new HashSet<NEWS_SUB_CATS>();
        }
    
        public int NEWS_CAT_ID { get; set; }
        public Nullable<int> SCHOOL_ID { get; set; }
        public string TITLE { get; set; }
        public string DESCRIPTION { get; set; }
        public string HOT_KEY { get; set; }
        public Nullable<System.DateTime> FROM_DATE { get; set; }
        public Nullable<System.DateTime> TO_DATE { get; set; }
        public Nullable<short> FROM_HOUR { get; set; }
        public Nullable<short> FROM_MINUTE { get; set; }
        public Nullable<short> TO_HOUR { get; set; }
        public Nullable<short> TO_MINUTE { get; set; }
        public byte[] PIC { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED_DATE { get; set; }
        public Nullable<int> MAIN_ID { get; set; }
    
        public virtual ICollection<ADMINS> ADMINS { get; set; }
        public virtual ICollection<CLASSES> CLASSES { get; set; }
        public virtual SCHOOLS SCHOOLS { get; set; }
        public virtual ICollection<NEWS_SUB_CATS> NEWS_SUB_CATS { get; set; }
    }
}
