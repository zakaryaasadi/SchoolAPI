using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class SessionClass
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime? expectedDate { get; set; }
        public List<LessonClass> lessons { get; set; }


        public SessionClass(SESSIONS session)
        {
            id = session.SESSION_ID;
            title = session.TITLE;
            expectedDate = session.EXPECTED_DATE;

        }


        public SessionClass(SESSIONS session, UserTypeClass userType)
        {
            id = session.SESSION_ID;
            title = session.TITLE;
            expectedDate = session.EXPECTED_DATE;
            lessons = session.LESSONS
                .OrderBy(s => s.ORDR)
                .Select(l => new LessonClass(l, userType)).ToList();

        }
    }
}