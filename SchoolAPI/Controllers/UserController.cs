using SchoolAPI.Models;
using SchoolAPI.Models.MD;
using SchoolAPI.Views.ModelView;
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
        //[HttpPost]
        //public HttpResponseMessage SignUp(int school_id, [FromBody] UserClass _user)
        //{
        //    try
        //    {
        //        using (Entities entities = new Entities())
        //        {
        //            USERS user = entities.USERS.Where(a => a.USER_NAME == _user.userName).FirstOrDefault();
        //            if (user != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 400, status = "Username already exists Please choose another username." });


        //            if (_user.password.Length < 6)
        //                return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 400, status = "Password less than 6 characters" });

        //            user = new USERS()
        //            {
        //                USER_NAME = _user.userName,
        //                USER_PASSWORD = _user.password,
        //                FULL_NAME = _user.fullName,
        //                IMAGE = _user.profileImage,
        //                SCHOOL_ID = school_id,
        //                TYPE = _user.type
        //            };
        //            entities.USERS.Add(user);
        //            entities.SaveChanges();

        //            return Request.CreateResponse(HttpStatusCode.Created, new Result() {statusCode = 200, status = "Done" , results = user});
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK,new Result() {statusCode = 400, status = ex.Message });
        //    }
        //}

        [HttpGet]
        public HttpResponseMessage SignIn(string user_name, string password)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    USERS user = entities.USERS.FirstOrDefault(a => a.USER_NAME == user_name && a.USER_PASSWORD == password);
                    if (user != null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = UserView.Info(entities, user) });

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
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = UserView.Info(entities, user) });
                    }
                        
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "Not Found !" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        //[HttpGet]
        //public HttpResponseMessage Info(int user_id)
        //{
        //    try
        //    {
        //        using (Entities entities = new Entities())
        //        {
        //            USERS _user = entities.USERS.Where(a => a.USER_ID == user_id).FirstOrDefault();
        //            if (_user != null)
        //            {
        //                UserClass userClass = new UserClass()
        //                {
        //                    id = _user.USER_ID,
        //                    userName = _user.USER_NAME,
        //                    fullName = _user.FULL_NAME,
        //                    password = _user.USER_PASSWORD,
        //                    profileImage = _user.IMAGE,


        //                };

        //                return Request.CreateResponse(HttpStatusCode.OK, new Result(){ statusCode = 200, status = "Done", results = userClass, num_result = 1 });
        //            }   

        //            return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "Not Found !" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
        //    }
        //}

        [HttpPost]
        public HttpResponseMessage SignInFB(int school_id, [FromBody] UserClass user)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    USERS _user = entities.USERS.FirstOrDefault(a => a.USER_NAME == user.userName);
                    if (user != null)
                    {
                        _user.IMAGE = user.profileImage;
                    }
                    else
                    {
                        _user = new USERS()
                        {
                            USER_NAME = user.userName,
                            USER_PASSWORD = user.password,
                            FULL_NAME = user.fullName,
                            IMAGE = user.profileImage,
                            SCHOOL_ID = school_id,
                          //  TYPE = _user.type
                        };
                        entities.USERS.Add(_user);
                    }
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, new Result() { statusCode = 200, status = "Done", results = user });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }
    }
}
