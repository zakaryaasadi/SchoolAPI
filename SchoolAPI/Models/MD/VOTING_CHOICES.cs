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
    
    public partial class VOTING_CHOICES
    {
        public VOTING_CHOICES()
        {
            this.USER_VOTES = new HashSet<USER_VOTES>();
        }
    
        public int VOTING_CHOICE_ID { get; set; }
        public Nullable<int> NEWS_ID { get; set; }
        public string CHOICE { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<int> MAIN_ID { get; set; }
    
        public virtual NEWS NEWS { get; set; }
        public virtual ICollection<USER_VOTES> USER_VOTES { get; set; }
    }
}
