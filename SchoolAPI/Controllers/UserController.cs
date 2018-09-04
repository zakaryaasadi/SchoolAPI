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
    public class UserController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage SignIn(string user_name, string password)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    USERS user = entities.USERS.FirstOrDefault(a => a.USER_NAME == user_name && a.USER_PASSWORD == password);
                    if (user != null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = new UserPrivateInfoClass(user) });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "Invalid Username or password !" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 400, status = ex.Message });
            }
        }

        [HttpPost]
        public HttpResponseMessage Update(int user_id, string full_name,[FromBody] byte[] image)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    USERS user = entities.USERS.FirstOrDefault(u => u.USER_ID == user_id);

                    if (user != null)
                    {
                        user.IMAGE = image; 
                        user.FULL_NAME = full_name;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = new UserPrivateInfoClass(user) });
                    }
                        
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "Not Found !" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int school_id, int page = 1)
        {
            try
            {
                using (Entities e = new Entities())
                {
                    var _userTotal = e.USERS.Where(u => u.SCHOOL_ID == school_id)
                        .OrderBy(u => u.TYPE);


                    var _users = _userTotal.Skip(5 * (page - 1)).Take(5);


                    List<UserPublicInfoClass> users = new List<UserPublicInfoClass>();
                    foreach (var item in _users)
                        users.Add(new UserPublicInfoClass(item));


                    int numTotalPage = (int)Math.Ceiling(_userTotal.Count() / 5.0);

                    if (page > numTotalPage)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, total_page = numTotalPage, status = "There is not page: " + page });

                    if (users.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, total_page = numTotalPage, status = "There are not users at this school", results = null });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, total_page = numTotalPage, num_result = users.Count, page = page, status = "Success", results = users });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }
    }
}
