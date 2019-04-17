using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class MessageSentClass : MessageClass
    {
        private Entities e = new Entities();
        public List<UserPublicInfoClass> toUser = new List<UserPublicInfoClass>();

        public MessageSentClass(MESSAGES message)
            :base(message)
        {
            fromUser = null;   
            foreach (var item in e.MESSAGE_RECIPIENTS.Where(m => m.MESSAGE_ID == message.MESSAGE_ID))
                toUser.Add(new UserPublicInfoClass(item.USERS));
        }
    }
}