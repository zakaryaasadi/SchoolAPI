using SchoolAPI.Models;
using SchoolAPI.Models.MD;
using SchoolAPI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolAPI.Controllers
{
    public class EventsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(int school_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Expression<Func<NEWS, bool>> expr = n => n.SCHOOL_ID == school_id && n.EVENT_DATE != null && n.EVENT_DATE >= DateTime.Now;
                    var numNews = entities.NEWS.Where(expr).Count();
                    int numTotalPage = (int)Math.Ceiling(numNews / 10.0);


                    var newsList = NewsView.getNewsClassList(entities, expr, 1);

                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, total_page = numTotalPage, status = "There are not events", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 200, total_page = numTotalPage, num_result = newsList.Count, page = 1, status = "Success", results = newsList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 400, status = ex.Message });
            }
        }
    }
}
