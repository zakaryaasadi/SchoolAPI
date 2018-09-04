using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolAPI.Models.MD;

namespace SchoolAPI.Models
{
    public class ChoiceClass
    {

        public int id { get; set; }
        public string title { get; set; }
        public int voteCount { get; set; }
        public bool isChoiced { get; set; }
        public int newsId { get; set; }

        public ChoiceClass(int newsId, int userId, VOTING_CHOICES voting_choice)
        {
            this.newsId = newsId;
            id = voting_choice.VOTING_CHOICE_ID;
            title = voting_choice.CHOICE;

            Entities e = new Entities();
            voteCount = e.USER_VOTES.Where(uv => uv.VOTING_CHOICE_ID == voting_choice.VOTING_CHOICE_ID).ToList().Count();
            isChoiced = e.USER_VOTES.FirstOrDefault(uv => uv.USER_ID == userId && uv.VOTING_CHOICE_ID == voting_choice.VOTING_CHOICE_ID) == null ? false : true;
        }
    }
}