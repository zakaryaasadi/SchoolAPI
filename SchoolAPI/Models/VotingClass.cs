using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolAPI.Models;

namespace SchoolAPI.Models
{
    public class VotingClass : NewsClass
    {
        public int totalVotes { get; set; }
        public List<ChoiceClass> choices { get; set; } 

        public short? voteType { get; set; }

        public short? voteResult { get; set; }
        public bool voteCount { get; set; }
        public DateTime? expierDate { get; set; }

    }
}