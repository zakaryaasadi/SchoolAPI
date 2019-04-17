using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class NoteClass
    {
        public int id { get; set; }
        public UserPublicInfoClass poster { get; set; }
        public string note { get; set; }
        public DateTime? date { get; set; }

        public NoteClass(STUDENTS_NOTES note)
        {
            this.id = note.STUDENT_NOTE_ID;
            this.poster = new UserPublicInfoClass(note.USER_ID);
            this.note = note.NOTE;
            this.date = note.NOTE_DATE;
        }
    }
}