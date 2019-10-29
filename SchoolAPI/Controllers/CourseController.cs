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
    public class CourseController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var user = new UserPrivateInfoClass(user_id);
                    var _courses = new List<COURSES>(); //.OrderBy(c => c.DAY_OF_THE_WEEK).OrderBy(c => c.ORDER_IN_WEEK)
                    var classId = user.classes.First().id;
                    switch (user.type.userType)
                    {
                        case UserType.Admin:
                            _courses = entities.COURSES.OrderBy(c => c.DAY_OF_THE_WEEK).ThenBy(c => c.ORDER_IN_WEEK).ToList();
                            break;
                        case UserType.Teacher:
                            _courses = entities.COURSES.Where(c => c.TEACHER_ID == user.type.id).OrderBy(c => c.DAY_OF_THE_WEEK).ThenBy(c => c.ORDER_IN_WEEK).ToList();
                            break;
                        case UserType.Student:
                            _courses = entities.COURSES.Where(c => c.CLASS_ID == classId).OrderBy(c => c.DAY_OF_THE_WEEK).ThenBy(c => c.ORDER_IN_WEEK).ToList();
                            break;
                    }
                    var courses = _courses.Select(s => new CourseClass(s)).ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = courses, numResult = courses.Count });
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }



    }
}
