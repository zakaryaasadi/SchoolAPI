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
    public class VotingController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(int school_id, int page = 1)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Expression<Func<NEWS, bool>> expr = n => n.SCHOOL_ID == school_id && n.TYPE == 3;
                    var numNews = entities.NEWS.Where(expr).Count();
                    int numTotalPage = (int)Math.Ceiling(numNews / 10.0);

                    if (page > numTotalPage)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 404, total_page = numTotalPage, status = "There is not page: " + page });


                    var newsList = NewsView.getVotingClassList(entities, expr, page);

                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, total_page = numTotalPage, status = "There are not voting", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 200, total_page = numTotalPage, num_result = newsList.Count, page = page, status = "Success", results = newsList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int school_id,int user_id, int page = 1)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Expression<Func<NEWS, bool>> expr = n => n.SCHOOL_ID == school_id && n.TYPE == 3;
                    var numNews = entities.NEWS.Where(expr).Count();
                    int numTotalPage = (int)Math.Ceiling(numNews / 10.0);

                    if (page > numTotalPage)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, total_page = numTotalPage, status = "There is not page: " + page });


                    var votingList = NewsView.getVotingClassList(entities, expr, page, user_id);

                    if (votingList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, total_page = numTotalPage, status = "There are not voting", results = votingList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, total_page = numTotalPage, num_result = votingList.Count, page = page, status = "Success", results = votingList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Vote(int user_id, int choice_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Expression<Func<USER_VOTES, bool>> expr = uv => uv.USER_ID == user_id && uv.VOTING_CHOICE_ID == choice_id;
                    var userVotes = entities.USER_VOTES.FirstOrDefault(expr);

                    if (userVotes != null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = "Already voted" });

                    USER_VOTES newUserVote = new USER_VOTES()
                    {
                        USER_VOTE_ID = entities.USER_VOTES.Max(v => v.USER_VOTE_ID) + 1,
                        USER_ID = user_id,
                        VOTING_CHOICE_ID = choice_id,
                        VOTING_DATE = DateTime.Now
                    };
                    
                    entities.USER_VOTES.Add(newUserVote);
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
        public HttpResponseMessage Del(int user_id, int choice_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Expression<Func<USER_VOTES, bool>> expr = uv => uv.USER_ID == user_id && uv.VOTING_CHOICE_ID == choice_id;
                    USER_VOTES userVotes = entities.USER_VOTES.FirstOrDefault(expr);

                    if (userVotes == null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "Not found" });

                    entities.USER_VOTES.Remove(userVotes);
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }
    }
}
