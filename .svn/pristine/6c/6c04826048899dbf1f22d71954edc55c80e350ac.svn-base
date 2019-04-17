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
    public class ExamController : ApiController
    {


        [HttpGet]
        public HttpResponseMessage Children(int user_id)
        {
            try
            {
                var user = new UserPrivateInfoClass(user_id);
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = user.children });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }



        [HttpGet]
        public HttpResponseMessage GetSubjects(int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var user = entities.USERS.FirstOrDefault(a => a.USER_ID == user_id);
                    if (user != null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = getExamSubject(user) });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "No exist user!" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


        [HttpGet]
        public HttpResponseMessage GetExams(int subject_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var _exams = entities.EXAMS.Where(ex => ex.SUBJECT_ID == subject_id).OrderByDescending(ex => ex.EXAM_ID);
                    if(_exams == null || _exams.ToList().Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "No exist exams!" });


                    List<ExamStudent> exams = new List<ExamStudent>();
                    foreach (var item in _exams)
                    {
                        var _student_exam = entities.STUDENTS_EXAM.Where(se => se.EXAM_ID == item.EXAM_ID);
                        List<Student> students = new List<Student>();
                        foreach(var _student in _student_exam)
                        {
                            students.Add(new Student(new UserPublicInfoClass(_student.STUDENTS.USER_ID), _student.GRADE, _student.ABSENT));
                        }
                        exams.Add(new ExamStudent(item.EXAM_ID, item.EXAM_NAME, item.EXAM_MAX, item.EXAM_PASS, item.EXAM_WEGHIT, students));
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "Done",results = exams });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }



        [HttpGet]
        public HttpResponseMessage GetStudents(int exam_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var _student_exam = entities.STUDENTS_EXAM.Where(se => se.EXAM_ID == exam_id);
                    var _exam = entities.EXAMS.FirstOrDefault(e => e.EXAM_ID == exam_id);
                    var _students = entities.STUDENT_SUBJECTS.Where(se => se.SUBJECT_ID == _exam.SUBJECT_ID);
                    List<SimpleStudent> students = new List<SimpleStudent>();
                    foreach (var _student in _students)
                        if (_student_exam.FirstOrDefault(se => se.STUDENT_ID == _student.STUDENT_ID) == null)
                            students.Add(new SimpleStudent() { id = _student.STUDENT_ID, name = _student.STUDENTS.USERS.USER_NAME });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "Done", results = students });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage AddExam(int user_id,int subject_id, int exam_type_id, int max, int min, string exam_name, DateTime date)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                   
                    if (new UserPublicInfoClass(user_id).type.userType == UserType.Parent || new UserPublicInfoClass(user_id).type.userType == UserType.Student)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "permission denial to add exam!" });


                    var isExistexam = entities.EXAMS.FirstOrDefault(e => e.EXAM_TYPE_ID == exam_type_id && e.SUBJECT_ID == subject_id);
                    if(isExistexam != null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "permission denial to add exam!" });

                    EXAMS exam = new EXAMS()
                    {
                        EXAM_ID = entities.EXAMS.Max(e => e.EXAM_ID) + 1,
                        SUBJECT_ID = subject_id,
                        EXAM_TYPE_ID = exam_type_id,
                        EXAM_NAME = exam_name,
                        EXAM_MAX = max,
                        EXAM_PASS = min,
                        EXAM_DATE = date
                    };
                    entities.EXAMS.Add(exam);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success"});
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


        [HttpGet]
        public HttpResponseMessage AddMark(int student_id, int exam_id, short absent, double mark)
        {
            try
            {
                using (Entities entities = new Entities())
                {

                    STUDENTS_EXAM stdExam = new STUDENTS_EXAM()
                    {
                        STUDENTS_EXAM_ID = entities.STUDENTS_EXAM.Max(e => e.STUDENTS_EXAM_ID) + 1,
                        EXAM_ID = exam_id,
                        STUDENT_ID = student_id,
                        ABSENT = absent,
                        GRADE = (decimal)mark
                    };
                    entities.STUDENTS_EXAM.Add(stdExam);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage GetExamType(int subject_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {

                    var _examTypes = entities.EXAM_TYPES.Where(e => e.SUBJECT_ID == subject_id);
                    if (_examTypes == null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "No exist exam!" });

                    List<SubjectClass> examTypes = new List<SubjectClass>();
                    foreach (var item in _examTypes)
                        examTypes.Add(new SubjectClass() {id = item.EXAM_TYPE_ID, name = item.NAME });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "Success", results = examTypes });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }
        private List<ExamSubject> getExamSubject(USERS user)
        {
            List<ExamSubject> list = new List<ExamSubject>();
            Entities e = new Entities();
            var _student = e.STUDENTS.FirstOrDefault(s => s.USER_ID == user.USER_ID);
            foreach (var s in new UserPrivateInfoClass(user).subjects)
            {
                var _exams = e.EXAMS.Where(ex => ex.SUBJECT_ID == s.id).OrderByDescending(ex => ex.EXAM_ID);
                if (_exams == null || _exams.ToList().Count == 0)
                    continue;

                List<Exam> exams = new List<Exam>();
                foreach (var item in _exams)
                {
                    var _student_exam = e.STUDENTS_EXAM.FirstOrDefault(se => se.STUDENT_ID == _student.STUDENT_ID && se.EXAM_ID == item.EXAM_ID);
                    exams.Add(new Exam(item.EXAM_ID, item.EXAM_NAME, _student_exam.GRADE, item.EXAM_MAX, item.EXAM_PASS, item.EXAM_WEGHIT, _student_exam.ABSENT));
                }
                list.Add(new ExamSubject(s, exams));
            }

            return list;
        }

    }

    class Exam
    {
        public int examId { get; set; }
        public string examName { get; set; }
        public decimal? mark { get; set; }
        public int? max { get; set; }
        public int? min { get; set; }
        public decimal? weight { get; set; }
        public short? absent { get; set; }

        public Exam (int id, string examName, decimal? mark, int? max, int? min, decimal? weight, short? absent)
        {
            examId = id;
            this.examName = examName;
            this.mark = mark;
            this.max = max;
            this.min = min;
            this.weight = weight;
            this.absent = absent;
        }
    }

    class ExamSubject
    {
        public SubjectClass subject { get; set; }
        public List<Exam> marks { get; set; }

        public ExamSubject(SubjectClass subject, List<Exam> marks)
        {
            this.subject = subject;
            this.marks = marks;
        }
    } 

    class ExamStudent
    {
        public int examId { get; set; }
        public string examName { get; set; }
        public int? max { get; set; }
        public int? min { get; set; }
        public decimal? weight { get; set; }
        public List<Student> students { get; set; }

        public ExamStudent(int id, string examName, int? max, int? min, decimal? weight, List<Student> students)
        {
            examId = id;
            this.examName = examName;
            this.max = max;
            this.min = min;
            this.weight = weight;
            this.students = students;
        }


    }

    class Student
    {
        public UserPublicInfoClass student { get; set; }
        public decimal? mark { get; set; }
        public short? absent { get; set; }

        public Student(UserPublicInfoClass student, decimal? mark, short? absent)
        {
            this.student = student;
            this.mark = mark;
            this.absent = absent;
        }

    }

    class SimpleStudent
    {
        public int? id { get; set; }
        public string name { get; set; }
    }
}
