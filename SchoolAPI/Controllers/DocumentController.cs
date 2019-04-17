﻿using SchoolAPI.Models;
using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolAPI.Controllers
{
    public class DocumentController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Get(int user_id, int subject_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var _doc = entities.DOCUMENT_USER.Where(s => s.USER_ID == user_id);

                    List<Document> docs = new List<Document>();
                    foreach (var item in _doc)
                    {
                        docs.Add(new Document()
                        {
                            id = item.DOCUMENT_ID,
                            name = item.DOCUMENTS.NAME,
                            questions = GetQuestion(item.DOCUMENTS)

                        });
                    }


                    var _std = entities.STUDENT_SUBJECTS.Where(s => s.SUBJECT_ID == subject_id);

                    List<UserPublicInfoClass> students = new List<UserPublicInfoClass>();
                    foreach (var item in _std)
                    {
                        UserPublicInfoClass std = new UserPublicInfoClass(item.STUDENTS.USER_ID);
                        std.profileImage = null;
                        students.Add(std);
                    }

                    R r = new R()
                    {
                        documents = docs,
                        students = students
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = r});
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }




        [HttpPost]
        public HttpResponseMessage Add(int user_id, int? subject_id, int? student_id, [FromBody] Document document)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    subject_id = subject_id == 0 ? null : subject_id;
                    student_id = student_id == 0 ? null : student_id;
                    foreach(DocQuestion q in document.questions)
                    {
                        if (q.type == 0)
                        {
                            ANSWERS_USERS _u = new ANSWERS_USERS()
                            {
                                ANSWER_USER_ID = entities.ANSWERS_USERS.Max(s => s.ANSWER_USER_ID) + 1,
                                USER_ID = user_id,
                                QUESTION_ID = q.id,
                                USER_ANSWER = q.value,
                                STUDENT_ID = student_id,
                                SUBJECT_ID = subject_id

                            };
                            entities.ANSWERS_USERS.Add(_u);
                            entities.SaveChanges();
                        }

                        else if (q.type == 1)
                        {
                            foreach (var a in q.answers)
                            {
                                if (a.isChecked)
                                {
                                    entities.ANSWERS_USERS.Add(new ANSWERS_USERS()
                                    {
                                        ANSWER_USER_ID = entities.ANSWERS_USERS.Max(s => s.ANSWER_USER_ID) + 1,
                                        USER_ID = user_id,
                                        QUESTION_ID = q.id,
                                        ANSWER_ID = a.id,
                                        STUDENT_ID = student_id,
                                        SUBJECT_ID = subject_id

                                    });
                                    break;
                                }
                                entities.SaveChanges();
                            }
                        }

                        else if(q.type == 2)
                        {
                            foreach(var a in q.answers)
                            {
                                if (a.isChecked)
                                    entities.ANSWERS_USERS.Add(new ANSWERS_USERS()
                                    {
                                        ANSWER_USER_ID = entities.ANSWERS_USERS.Max(s => s.ANSWER_USER_ID) + 1,
                                        USER_ID = user_id,
                                        QUESTION_ID = q.id,
                                        ANSWER_ID = a.id,
                                        STUDENT_ID = student_id,
                                        SUBJECT_ID = subject_id

                                    });
                                entities.SaveChanges();
                            }
                        }
                    }


                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        List<DocAnswer> GetAnswers(DOC_QUESTIONS q)
        {
            List<DocAnswer> a = new List<DocAnswer>();
            foreach (var item in q.DOC_ANSWERS)
            {
                a.Add(new DocAnswer()
                {
                    id = item.ANSWER_ID,
                    name = item.VALUE
                });
            }
            return a;
        }

        List<DocQuestion> GetQuestion(DOCUMENTS d)
        {
            List<DocQuestion> a = new List<DocQuestion>();
            foreach (var item in d.DOC_QUESTIONS)
            {
                a.Add(new DocQuestion()
                {
                    id = item.QUESTION_ID,
                    name = item.VALUE,
                    type = item.TYPE,
                    answers = GetAnswers(item)
                });
            }
            return a;
        }
    }


    public class R
    {
        public List<Document> documents { get; set; }
        public List<UserPublicInfoClass> students { get; set; }
    }

    public class Document
    {
        public int? id { get; set; }
        public string name { get; set; }
        public List<DocQuestion> questions { get; set; }
    }

    public class DocAnswer {
        public int id { get; set; }
        public string name { get; set; }
        public bool isChecked { get; set; }
    }
    public class DocQuestion {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public string value { get; set; }
        public List<DocAnswer> answers { get; set; }
    }
}

