using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class AttachmentClass
    {
        public int id { get; set; }
        public string name { get; set; }
        public AttachmentType type { get; set; }
        public byte[] attach { get; set; }

        public AttachmentClass(ATTACHMENTS attachment)
        {
            id = attachment.ATTACH_ID;
            name = attachment.ATTACH_FILENAME;
            type = getType();
        }

        public ATTACHMENTS GetAttahmentDB()
        {
            Entities e = new Entities();
            return new ATTACHMENTS()
            {
                ATTACH_ID = e.ATTACHMENTS.Max(a => a.ATTACH_ID) + 1,
                ATTACH_FILENAME = name,
                ATTACHMENT = attach
            };
        }

        private AttachmentType getType()
        {
            string extension = name.Split(new char[] { '.' }).LastOrDefault();
            extension = extension.ToLower();
            switch (extension)
            {
                case "png":
                case "jpg":
                case "jpeg":
                case "gif":
                    return AttachmentType.IMAGE;
                    

                //case "mp3":
                //case "wav":
                //    return AttachmentType.Audio;
                    

                //case "mp4":
                //    return AttachmentType.VIDEO;
                    

                default:
                    return AttachmentType.File;
                    
            }
        }

    }

    public enum AttachmentType
    {
        IMAGE, File 
    }
}