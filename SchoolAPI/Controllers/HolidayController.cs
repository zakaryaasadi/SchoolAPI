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
    public class HolidayController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Get(int school_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {

                    var _holidaysTotal = entities.HOLYDAYS
                        .Where(m => m.SCHOOL_ID == school_id)
                        .OrderByDescending(m => m.HOLYDAY_ID).ToList()
                        .Select(m => new HolidayClass(m));

                    if (_holidaysTotal.Count() == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "There are not holidays for you", results = _holidaysTotal });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, numResult = _holidaysTotal.Count(), status = "Success", results = _holidaysTotal });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }
    }
}
