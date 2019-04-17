using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class SubjectClass
    {
        public int id { get; set; }
        public string name { get; set; }

        public SubjectClass(SUBJECTS subject)
        {
            if (subject == null)
                return;

            id = subject.SUBJECT_ID;
            name = subject.SUBJECT_NAME;
        }


        public SubjectClass(int? subjectId)
        {
            if (subjectId == null)
                return;
            Entities e = new Entities();
            SUBJECTS subject = e.SUBJECTS.FirstOrDefault(s => s.SUBJECT_ID == subjectId);
            id = subject.SUBJECT_ID;
            name = subject.SUBJECT_NAME;
        }

    }
}