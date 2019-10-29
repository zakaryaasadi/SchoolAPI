using SchoolAPI.Models;
using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SchoolAPI.Controllers
{
    public class LessonController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetSubject(int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    USERS user = entities.USERS.FirstOrDefault(a => a.USER_ID == user_id);
                    if (user != null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = new UserPrivateInfoClass(user).classes });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "No exist user!" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int user_id, int subject_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var userType = new UserPublicInfoClass(entities.USERS.FirstOrDefault(u => u.USER_ID == user_id)).type;
                    var sessions = entities.SESSIONS.ToList().Where(a => a.SUBJECT_ID == subject_id)
                        .OrderBy(s => s.ORDR)
                        .Select(s => new SessionClass(s, userType)).ToList();
                    if (sessions != null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = sessions });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "No exist user!" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }



        [HttpGet]
        public HttpResponseMessage Get(int user_id, int subject_id, DateTime start_date, DateTime end_date)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var userType = new UserPublicInfoClass(entities.USERS.FirstOrDefault(u => u.USER_ID == user_id)).type;
                    var sessions = entities.SESSIONS.ToList().Where(a => a.SUBJECT_ID == subject_id && a.EXPECTED_DATE >= start_date && a.EXPECTED_DATE <= end_date)
                        .OrderBy(s => s.ORDR)
                        .Select(s => new SessionClass(s, userType)).ToList();
                    if (sessions != null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = sessions });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "No exist user!" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


        [HttpGet]
        public HttpResponseMessage GetEducationyear(int school_id)
        {
            try
            {
                using (Entities e = new Entities())
                {

                    var pk = e.EDUCATION_YEARS.ToList().Count == 0 ? 0 : e.EDUCATION_YEARS.Max(s => s.EDUCATION_YEAR_ID);
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done" , results = pk});
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }



        [HttpGet]
        public HttpResponseMessage AddSession(int school_id, int class_id, int subject_id, int education_year_id, string title, string objective, short order)
        {
            try
            {
                using (Entities e = new Entities())
                {
                    var pk = e.SESSIONS.ToList().Count == 0 ? 0 : e.SESSIONS.Max(s => s.SESSION_ID);
                    SESSIONS _session = new SESSIONS()
                    {
                        SESSION_ID = pk + 1,
                        SCHOOL_ID = school_id,
                        CLASS_ID = class_id,
                        SUBJECT_ID = subject_id,
                        EDUCATION_YEAR_ID = education_year_id,
                        TITLE = title,
                        OBJECTIVE = objective,
                        ORDR = order
                    };
                    e.SESSIONS.Add(_session);
                    e.SaveChanges();

                    var session = new SessionClass(_session);
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done" , results = session});
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }




        [HttpGet]
        public HttpResponseMessage AddHeadline(int school_id, int session_id, string title, string objective, string note)
        {
            try
            {
                using (Entities e = new Entities())
                {

                    var pk = e.HEADLINES.ToList().Count == 0 ? 0 : e.HEADLINES.Max(s => s.HEADLINE_ID);
                    var _session = e.SESSIONS.First(s => s.SESSION_ID == session_id);
                    HEADLINES _headline = new HEADLINES()
                    {
                        SESSION_ID = _session.SESSION_ID,
                        TITLE = title,
                        OBJECTIVE = objective,
                        NOTE = note

                    };

                    e.HEADLINES.Add(_headline);
                    e.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = _headline.HEADLINE_ID });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }



        [HttpGet]
        public HttpResponseMessage Addlesson(int session_id, string title, string objective, short order, short duration, DateTime issue_date)
        {
            try
            {
                using (Entities e = new Entities())
                {
                    var pk = e.LESSONS.ToList().Count == 0 ? 0 : e.LESSONS.Max(s => s.LESSON_ID);
                    var _session = e.SESSIONS.First(s => s.SESSION_ID == session_id);
                    LESSONS _lession = new LESSONS()
                    {
                        LESSON_ID = pk + 1,
                        SESSION_ID = session_id,
                        SCHOOL_ID = _session.SCHOOL_ID,
                        CLASS_ID = _session.CLASS_ID,
                        SUBJECT_ID = _session.SUBJECT_ID,
                        EDUCATION_YEAR_ID = _session.EDUCATION_YEAR_ID,
                        TITLE = title,
                        OBJECTIVE = objective,
                        ORDR = order,
                        DURATION = duration,
                        ISSUE_DATE = issue_date
                    };
                    e.LESSONS.Add(_lession);
                    e.SaveChanges();


                    pk = e.SUB_HEADLINES.ToList().Count == 0 ? 0 : e.SUB_HEADLINES.Max(s => s.SUB_HEADLINE_ID);
                    SUB_HEADLINES _sub = new SUB_HEADLINES()
                    {
                        SUB_HEADLINE_ID = pk + 1,
                        HEADLINE_ID = _session.HEADLINES.First().HEADLINE_ID,
                        TITLE = title,
                        OBJECTIVE = objective
                    };
                    e.SUB_HEADLINES.Add(_sub);
                    e.SaveChanges();

                    var lesson = new LessonClass(_lession);
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = lesson });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IHttpActionResult> AddFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();

            var lesson_id = Request.Headers.GetValues("lesson_id").First();
            int lessonId = int.Parse(lesson_id);

            await Request.Content.ReadAsMultipartAsync(provider);
            Entities e = new Entities();
            var _lesson = e.LESSONS.FirstOrDefault(m => m.LESSON_ID == lessonId);
            
            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var buffer = await file.ReadAsByteArrayAsync();
                //Do whatever you want with filename and its binary data.


                var pk = e.RESOURCES.ToList().Count == 0 ? 0 : e.RESOURCES.Max(s => s.RESOURCE_ID);
                RESOURCES _res = new RESOURCES()
                {
                    RESOURCE_ID = pk + 1,
                    SCHOOL_ID = _lesson.SCHOOL_ID,
                    EDUCATION_YEAR_ID = _lesson.EDUCATION_YEAR_ID,
                    NAME = filename,
                    OBJECTIVE = _lesson.OBJECTIVE,
                    ATTACHMENT_NAME = filename,
                    ATTACHMENT_EXT = filename.Split(new char[] { '.' })[filename.Split(new char[] { '.' }).Length - 1]
                };
                e.RESOURCES.Add(_res);
                e.SaveChanges();


                pk = e.LESSONS_SECTIONS.ToList().Count == 0 ? 0 : e.LESSONS_SECTIONS.Max(s => s.LESSON_SECTION_ID);
                LESSONS_SECTIONS _ls = new LESSONS_SECTIONS()
                {
                    LESSON_SECTION_ID = pk + 1,
                    LESSON_ID = lessonId,
                    RESOURCE_ID = _res.RESOURCE_ID,
                    ORDR = _lesson.ORDR,
                    DURATION = _lesson.DURATION,
                    TEACHER = 1,
                    STUDENTS = 1
                };
                e.LESSONS_SECTIONS.Add(_ls);
                e.SaveChanges();


                pk = e.RESOURCES_LINKS.ToList().Count == 0 ? 0 : e.RESOURCES_LINKS.Max(s => s.RESOURCE_LINK_ID);
                RESOURCES_LINKS _rl = new RESOURCES_LINKS()
                {
                    RESOURCE_LINK_ID = pk + 1,
                    RESOURCE_ID = _res.RESOURCE_ID,
                    LINK_ID = _lesson.SESSION_ID,
                    LINK_WITH = 0
                };
                e.RESOURCES_LINKS.Add(_rl);
                e.SaveChanges();

            }
            



            return Ok();
        }


        [HttpGet]
        public HttpResponseMessage GetFile(int file_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var _attach = entities.RESOURCES
                        .FirstOrDefault(a => a.RESOURCE_ID == file_id);

                    if (_attach == null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "There are not messages for you", results = null });

                    //Create HTTP Response.
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                                                                                                                                                                                                                                                                                                          
                    byte[] bytes = _attach.ATTACHMENT;
                    string fileName = _attach.ATTACHMENT_NAME + _attach.ATTACHMENT_EXT;

                    //Set the Response Content.
                    response.Content = new ByteArrayContent(bytes);

                    //Set the Response Content Length.
                    response.Content.Headers.ContentLength = bytes.LongLength;

                    //Set the Content Disposition Header Value and FileName.
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));
                    return response;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


        [HttpGet]
        public HttpResponseMessage GetExist(int session_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var students = entities.SESSIONS.FirstOrDefault(s => s.SESSION_ID == session_id).SUBJECTS.STUDENT_SUBJECTS;
                    var result = new List<STD>();
                    var studentSession = entities.SESSIONS.FirstOrDefault(s => s.SESSION_ID == session_id).STUDENT_SESSION_DETAILS;
                    foreach (var s in students)
                    {
                        var r = new STD() { id = s.STUDENTS.STUDENT_ID, name = s.STUDENTS.USERS.FULL_NAME, later = 0, exist = 1 , note = ""};
                        var x = studentSession.FirstOrDefault(ss => ss.STUDENT_ID == s.STUDENT_ID);
                        if(x != null)
                        {
                            r.later =  x.LATE;
                            r.exist = x.EXIST;
                            r.note = x.NOTES == null ? "" : x.NOTES;
                        }
                        result.Add(r);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = result.ToList() });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpPost]
        public HttpResponseMessage AddExist(int session_id, [FromBody] List<STD> students)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    foreach(var s in students)
                    {
                        var x = entities.STUDENT_SESSION_DETAILS.FirstOrDefault(ss => ss.STUDENT_ID == s.id && ss.SESSION_ID == session_id);
                        if(x != null) //edit
                        {
                            x.LATE = s.later;
                            x.EXIST = s.exist;
                            x.NOTES = s.note;
                        }
                        else
                        {
                            entities.STUDENT_SESSION_DETAILS.Add(new STUDENT_SESSION_DETAILS()
                            {
                                STU_SESS_ID = entities.STUDENT_SESSION_DETAILS.Max(ss => ss.STU_SESS_ID) + 1,
                                SESSION_ID = session_id,
                                STUDENT_ID = s.id,
                                LATE = s.later,
                                EXIST = s.exist,
                                NOTES = s.note,
                            });
                        }
                        
                    }


                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done"});
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }
    }

    public class STD
    {
        public int id { get; set; }
        public string name { get; set; }
        public short later { get; set; }
        public short exist { get; set; }
        public string note { get; set; }
    }

}
