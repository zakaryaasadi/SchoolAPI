﻿using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class AttachmentClass
    {
        public int id { get; set; }
        public int messageId { get; set; }
        public string name { get; set; }
        public AttachmentType type { get; set; }
        public byte[] attach { get; set; }

        public string size { get; set; }

        public AttachmentClass() { }

        public AttachmentClass(ATTACHMENTS attachment)
        {
            id = attachment.ATTACH_ID;
            name = attachment.ATTACH_FILENAME;
            type = getType();
            size = getSize(attachment.ATTACHMENT);
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
            if (name == null)
                return AttachmentType.File;
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

        private string getSize(byte[] file)
        {
            if (file == null)
                return "0 Byte";
            float Bytes = file.Length;
            float KB = Bytes / 1024;
            float MB = KB / 1024;
            float GB = MB / 1024;

            if (GB > 1)
                return (Math.Round(GB * 100.0) / 100.0).ToString() + " GB";
            else if (MB > 1)
                return (Math.Round(MB * 100.0) / 100.0).ToString() + " MB";
            else if (KB > 1)
                return (Math.Round(KB * 100.0) / 100.0).ToString() + " KB";

            return (Math.Round(Bytes * 100.0) / 100.0).ToString() + " Byte";
        }

    }

    public enum AttachmentType
    {
        IMAGE, File 
    }
}