using SchoolAPI.Models;
using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolAPI.Controllers
{
    public class NoteController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetStudents(int user_id)
        {
            try
            {
                UserPrivateInfoClass user = new UserPrivateInfoClass(user_id);
                switch (user.type.userType)
                {
                    case UserType.Parent:
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = user.children });
                    case UserType.Teacher:
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = user.students });
                    case UserType.Student:
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = new UserPublicInfoClass(user.id) });
                    default:
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results =null });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int student_id)
        {
            try
            {
                using (Entities e = new Entities())
                {
                    List<NoteClass> notes = new List<NoteClass>();
                    var _notes = e.STUDENTS_NOTES.Where(s => s.STUDENTS.USER_ID == student_id).OrderByDescending(s => s.NOTE_DATE);
                    foreach (var note in _notes)
                        notes.Add(new NoteClass(note));
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = notes });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Add(int user_id, int student_id, string note)
        {
            try
            {
                using (Entities e = new Entities())
                {
                    var pk = e.STUDENTS_NOTES.ToList().Count == 0 ? 0 : e.STUDENTS_NOTES.Max(s => s.STUDENT_NOTE_ID);
                    STUDENTS_NOTES _note = new STUDENTS_NOTES()
                    {
                        STUDENT_NOTE_ID = pk + 1,
                        STUDENT_ID = e.STUDENTS.FirstOrDefault(s => s.USER_ID == student_id).STUDENT_ID,
                        USER_ID = user_id,
                        NOTE = note,
                        NOTE_DATE = DateTime.Now
                    };
                    e.STUDENTS_NOTES.Add(_note);
                    e.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }



        [HttpGet]
        public HttpResponseMessage AddNote(int student_id, int session_id, string note)
        {
            try
            {
                using (Entities e = new Entities())
                {
                    var pk = e.STUDENT_SESSION_DETAILS.ToList().Count == 0 ? 0 : e.STUDENT_SESSION_DETAILS.Max(s => s.STU_SESS_ID);
                    STUDENT_SESSION_DETAILS _note = new STUDENT_SESSION_DETAILS()
                    {
                        STU_SESS_ID = pk + 1,
                        STUDENT_ID = e.STUDENTS.FirstOrDefault(s => s.USER_ID == student_id).STUDENT_ID,
                        NOTES = note,
                        SESSION_ID = session_id
                     };
                    e.STUDENT_SESSION_DETAILS.Add(_note);
                    e.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

    }
}
