using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolAPI.Models.MD;
using SchoolAPI.Models;
using SchoolAPI.Views;
using System.Linq.Expressions;
using SchoolAPI.Views.ModelView;

namespace SchoolAPI.Controllers
{
    public class NewsController : ApiController
    {
        //////////NEWS////////
        [HttpGet]
        public HttpResponseMessage Get(int school_id, int type = 2, int page = 1)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Expression<Func<NEWS, bool>> expr = n => n.SCHOOL_ID == school_id && n.TYPE == type && n.EVENT_DATE == null && n.ACCESSIBILITY == 1;
                    var numNews = entities.NEWS.Where(expr).Count();
                    int numTotalPage = (int)Math.Ceiling(numNews / 10.0);

                    if (page > numTotalPage)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 400, total_page = numTotalPage, status = "There is not page: " + page });


                    var newsList = NewsView.getNewsClassList(entities,expr ,page);

                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 404, total_page = numTotalPage, status = "There are not news", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, total_page = numTotalPage, num_result = newsList.Count, page = page, status = "Success", results = newsList });
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400,  status = ex.Message });
            }
        }



        [HttpGet]
        public HttpResponseMessage Get(int school_id, int sub_cat_id, int type = 2, int page = 1)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Expression<Func<NEWS, bool>> expr = n => n.SCHOOL_ID == school_id && n.NEWS_SUB_CAT_ID == sub_cat_id && n.TYPE == type && n.EVENT_DATE == null && n.ACCESSIBILITY == 1;
                    var numNews = entities.NEWS.Where(expr).Count();
                    int numTotalPage = (int)Math.Ceiling(numNews / 10.0);

                    if (page > numTotalPage)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, total_page = numTotalPage, status = "There is not page: " + page });


                    var newsList = NewsView.getNewsClassList(entities, expr, page);

                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, total_page = numTotalPage, status = "There are not news", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, total_page = numTotalPage, num_result = newsList.Count, page = page, status = "Success", results = newsList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


        [HttpGet]
        public HttpResponseMessage Profile(int school_id, int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Expression<Func<NEWS, bool>> expr = n => n.SCHOOL_ID == school_id && n.USER_ID == user_id && n.ACCESSIBILITY == 1;
                    var numNews = entities.NEWS.Where(expr).Count();
                    int numTotalPage = (int)Math.Ceiling(numNews / 10.0);


                    var newsList = NewsView.getNewsClassList(entities, expr, 1);

                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, total_page = numTotalPage, status = "There are not news", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, total_page = numTotalPage, num_result = newsList.Count, page = 1, status = "Success", results = newsList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Group(int school_id, int sub_cat_id, int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    List<NewsClass> newsList = new List<NewsClass>();
                    USERS user = entities.USERS.FirstOrDefault(u => u.USER_ID == user_id);
                    if(user != null )
                        foreach (var group in UserView.Info(entities,user).groups )
                        {
                            var rowNews = entities.NEWS_ACL_GROUPS.Where(n => n.GROUP_ID == group.id).ToList();
                            foreach (var news in rowNews.Where(n => n.NEWS.NEWS_SUB_CAT_ID == sub_cat_id))
                                newsList.Add(NewsView.GetNewsFromRowNews(entities, news.NEWS));
                        }
                    

                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, total_page = newsList.Count, status = "There are not news", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, total_page = newsList.Count, num_result = newsList.Count, page = 1, status = "Success", results = newsList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Class(int school_id, int sub_cat_id, int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    List<NewsClass> newsList = new List<NewsClass>();
                    USERS user = entities.USERS.FirstOrDefault(u => u.USER_ID == user_id);
                    if (user != null)
                        foreach (var _class in UserView.Info(entities, user).classes)
                        {
                            var rowNews = entities.NEWS_ACL_CLASSES.Where(n => n.CLASS_ID == _class.id).ToList();
                            foreach (var news in rowNews.Where(n => n.NEWS.NEWS_SUB_CAT_ID == sub_cat_id))
                                newsList.Add(NewsView.GetNewsFromRowNews(entities, news.NEWS));
                        }


                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, total_page = newsList.Count, status = "There are not news", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, total_page = newsList.Count, num_result = newsList.Count, page = 1, status = "Success", results = newsList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Subject(int school_id, int sub_cat_id, int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    List<NewsClass> newsList = new List<NewsClass>();
                    USERS user = entities.USERS.FirstOrDefault(u => u.USER_ID == user_id);
                    if (user != null)
                        foreach (var _subject in UserView.Info(entities, user).subjects)
                        {
                            var rowNews = entities.NEWS_ACL_SUBJECTS.Where(n => n.SUBJECT_ID == _subject.id).ToList();
                            foreach (var news in rowNews.Where(n => n.NEWS.NEWS_SUB_CAT_ID == sub_cat_id))
                                newsList.Add(NewsView.GetNewsFromRowNews(entities, news.NEWS));
                        }


                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, total_page = newsList.Count, status = "There are not news", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, total_page = newsList.Count, num_result = newsList.Count, page = 1, status = "Success", results = newsList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


        /////////New Category////

        [HttpGet]
        public HttpResponseMessage Cats(int school_id)
        {
            using (Entities entities = new Entities())
            {
                var newsCatsList = entities.NEWS_CATS
                    .Include("NEWS_SUB_CATS")
                    .Where(c => c.SCHOOL_ID == school_id).ToList();

                if (newsCatsList == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "There are not news categories", results = newsCatsList });

                List<CategoryClass> catsList = new List<CategoryClass>();
                foreach (var item in newsCatsList)
                {
                    catsList.Add(new CategoryClass()
                    {
                        id = item.NEWS_CAT_ID,
                        title = item.TITLE,
                        image = item.PIC,
                        subcategories = SubcategoriesView.getSubcategories(item.NEWS_SUB_CATS.ToList())
                    });

                }

                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, num_result = catsList.Count, status = "Success", results = catsList });
            }
        }

        [HttpGet]
        public HttpResponseMessage SubCats(int news_cat_id)
        {
            using (Entities entities = new Entities())
            {
                var newsSubCatsList = entities.NEWS_SUB_CATS.
                    Where(s => s.NEWS_CAT_ID == news_cat_id)
                    .ToList();

                if (newsSubCatsList == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "There are not news sub category", results = newsSubCatsList });

                var subCatsList = SubcategoriesView.getSubcategories(newsSubCatsList);

                return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 200,num_result = subCatsList.Count, status = "Success", results = subCatsList });
            }
        }

    }
}
