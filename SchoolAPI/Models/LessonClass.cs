using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class LessonClass
    {
        
        public string title { get; set; }

        public int duration { get; set; }
        public List<Resource> files { get; set; }


        public LessonClass(LESSONS lesson)
        {
            title = lesson.TITLE;
            duration = lesson.DURATION == null ? 0 : lesson.DURATION.Value;

        }

        public LessonClass(LESSONS lesson, UserTypeClass userType)
        {
            title = lesson.TITLE;
            duration = lesson.DURATION == null ? 0 : lesson.DURATION.Value;
            if(userType.userType == UserType.Teacher)
            {
                files = lesson.LESSONS_SECTIONS
                .Where(ls => ls.TEACHER == 1)
                .Select(ls => new Resource(ls.RESOURCES)).ToList();
            }
            else
            {
                files = lesson.LESSONS_SECTIONS
                .Where(ls => ls.STUDENTS == 1)
                .Select(ls => new Resource(ls.RESOURCES)).ToList();
            }
            
        }
    }

    public class Resource
    {
        public int id { get; set; }
        public string title { get; set; }
        public string type { get; set; }

        public string size { get; set; }

        public Resource(RESOURCES r)
        {
            id = r.RESOURCE_ID;
            title = r.ATTACHMENT_NAME;
            type = r.ATTACHMENT_EXT;
            size = getSize(r.ATTACHMENT);
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
}