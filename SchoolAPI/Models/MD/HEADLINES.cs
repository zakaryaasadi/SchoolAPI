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
    
    public partial class HEADLINES
    {
        public HEADLINES()
        {
            this.SUB_HEADLINES = new HashSet<SUB_HEADLINES>();
        }
    
        public int HEADLINE_ID { get; set; }
        public Nullable<int> SESSION_ID { get; set; }
        public string TITLE { get; set; }
        public string OBJECTIVE { get; set; }
        public string NOTE { get; set; }
    
        public virtual SESSIONS SESSIONS { get; set; }
        public virtual ICollection<SUB_HEADLINES> SUB_HEADLINES { get; set; }
    }
}
