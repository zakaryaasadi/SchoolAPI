using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class MessageClass
    {
        private Entities e = new Entities();
        public int id { get; set; }
        public UserPublicInfoClass fromUser { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public DateTime? date { get; set; }
        public GroupClass group { get; set; }
        public SubjectClass subject { get; set; }
        public List<AttachmentClass> attachments { get; set; }

        public MessageClass(MESSAGES message)
        {
            id = message.MESSAGE_ID;
            fromUser = new UserPublicInfoClass(message.FROM_USER_ID);
            title = message.MESSAGE_SUBJECT;
            body = message.MESSAGE_CONTENT;
            date = message.MESSAGE_DATE;
            group = message.GROUP_ID == null ? null : new GroupClass(message.GROUP_ID);
            subject = message.SUBJECT_ID == null ? null : new SubjectClass(message.SUBJECT_ID);
            attachments = getAttachments();
            
        }

        public MESSAGES GetMessageDB()
        {
            return new MESSAGES()
            {
                MESSAGE_ID = e.MESSAGES.Max(n => n.MESSAGE_ID) + 1,
                FROM_USER_ID = fromUser.id,
                MESSAGE_SUBJECT = title,
                MESSAGE_CONTENT = body,
                MESSAGE_DATE = DateTime.Now,
                CREATION_DATE = DateTime.Now,
                GROUP_ID = group.id,
                SUBJECT_ID = subject.id,
                MESSAGE_TYPE = 2,
                MESSAGE_STATUS = 1
            };
        }

        private List<AttachmentClass> getAttachments()
        {
            List<AttachmentClass> attachments = new List<AttachmentClass>();
            
            var _attachments = e.ATTACHMENTS.Where(a => a.MESSAGE_ID == id);

            foreach (var item in _attachments)
                attachments.Add(new AttachmentClass(item));

            return attachments;
        }
    }
}