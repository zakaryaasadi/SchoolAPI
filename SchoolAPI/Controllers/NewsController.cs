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
using SchoolAPI.ModelView;
using System.Threading.Tasks;
using System.Web;

namespace SchoolAPI.Controllers
{
    public class NewsController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Slogen(int school_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    SCHOOLS _school = entities.SCHOOLS.FirstOrDefault(s => s.SCHOOL_ID == school_id);
                    return Request.CreateResponse(HttpStatusCode.OK, _school.SCHOOL_NAME);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        //////////NEWS////////
        //[HttpGet]
        //public HttpResponseMessage Get(int school_id, int type = 2, int page = 1)
        //{
        //    try
        //    {
        //        using (Entities entities = new Entities())
        //        {
        //            Expression<Func<NEWS, bool>> expr = n => n.SCHOOL_ID == school_id && n.TYPE == type && n.EVENT_DATE == null && n.ACCESSIBILITY == 1;
        //            var numNews = entities.NEWS.Where(expr).Count();
        //            int numTotalPage = (int)Math.Ceiling(numNews / 10.0);

        //            if (page > numTotalPage)
        //                return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 400, totalPage = numTotalPage, status = "There is not page: " + page });


        //            var newsList = NewsView.getNewsClassList(entities,expr ,page);

        //            if (newsList.Count == 0)
        //                return Request.CreateResponse(HttpStatusCode.OK, new Result() {statusCode = 404, totalPage = numTotalPage, status = "There are not news", results = newsList });

        //            return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, totalPage = numTotalPage, numResult = newsList.Count, page = page, status = "Success", results = newsList });
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400,  status = ex.Message });
        //    }
        //}



        [HttpGet]
        public HttpResponseMessage Get(int school_id, int sub_cat_id, int page = 1)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Expression<Func<NEWS, bool>> expr = n => n.SCHOOL_ID == school_id && n.NEWS_SUB_CAT_ID == sub_cat_id && n.TYPE != 3 && n.EVENT_DATE == null && n.ACCESSIBILITY == 1 ;
                    var numNews = entities.NEWS.Where(expr).Count();
                    int numTotalPage = (int)Math.Ceiling(numNews / 5.0);

                    if (page > numTotalPage)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, totalPage = numTotalPage, status = "There is not page: " + page });


                    var newsList = NewsView.getNewsClassList(entities, expr, page).OrderBy(n => n.type).ToList();

                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, totalPage = numTotalPage, status = "There are not news", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, totalPage = numTotalPage, numResult = numNews, page = page, status = "Success", results = newsList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


        [HttpGet]
        public HttpResponseMessage Detail(int news_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    List<byte[]> images = new List<byte[]>();
                    var _media = entities.NEWS_MEDIAS.Where(n => n.NEWS_ID == news_id && n.TYPE == 2);
                    foreach (var i in _media)
                        if (i.ATTACH != null)
                            images.Add(Compression.ImageCompression(i.ATTACH, System.Drawing.Imaging.ImageFormat.Jpeg));

                    //delete first image becouse before send
                    if(images.Count > 0)
                        images.RemoveAt(0);

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = images });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpPost]
        public HttpResponseMessage Add(int school_id, [FromBody] NewsClass news)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    if (entities.ALLOWED_CATS.FirstOrDefault(a => a.NEWS_SUB_CAT_ID == news.subcategory.id && a.USER_ID == news.userId) == null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = "You do not have permission to add news to this subcategory, please request permission from the administrator" });

                    NEWS _News = NewsView.CreateNews(entities, news);
                    _News.SCHOOL_ID = school_id; 
                    
                    entities.NEWS.Add(_News);
                    entities.SaveChanges();

                    NewsClass newNews = NewsView.GetNews(entities.NEWS.FirstOrDefault(n => n.NEWS_ID == _News.NEWS_ID));
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = new List<NewsClass> { newNews } });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


        [HttpGet]
        public HttpResponseMessage GetPermission(int cat_id, int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    List<SubcategoryClass> sub = new List<SubcategoryClass>();
                    foreach (var item in entities.ALLOWED_CATS.Where(a => a.NEWS_SUB_CATS.NEWS_CAT_ID == cat_id && a.USER_ID == user_id))
                        sub.Add(new SubcategoryClass(item.NEWS_SUB_CATS));
                        
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Done", results = sub });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IHttpActionResult> AddImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();

            var news_id = Request.Headers.GetValues("news_id").First();
            int newsId = int.Parse(news_id);


            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var buffer = await file.ReadAsByteArrayAsync();
                //Do whatever you want with filename and its binary data.
                using (Entities e = new Entities())
                {
                    var _media = new NEWS_MEDIAS() { NEWS_MEDIA_ID = e.NEWS_MEDIAS.Max(n => n.NEWS_MEDIA_ID) + 1, NEWS_ID = newsId, ATTACH = buffer, TYPE = 2, NAME = filename };
                    e.NEWS_MEDIAS.Add(_media);
                    e.SaveChanges();
                }

            }
            return Ok();
        }


        [HttpPost]
        public async Task<IHttpActionResult> AddVideo()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();

            var news_id = Request.Headers.GetValues("news_id").First();
            int newsId = int.Parse(news_id);


            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var buffer = await file.ReadAsByteArrayAsync();
                //Do whatever you want with filename and its binary data.
                using (Entities e = new Entities())
                {
                    var id = e.NEWS_MEDIAS.Max(n => n.NEWS_MEDIA_ID) + 1;
                    var fileName = id + ".mp4";
                    var _media = new NEWS_MEDIAS() { NEWS_MEDIA_ID = id, NEWS_ID = newsId, TYPE = 3, NAME = fileName };
                    e.NEWS_MEDIAS.Add(_media);
                    e.SaveChanges();

                    string path = HttpContext.Current.Server.MapPath("~");
                    string pathVideo = path + @"videos\" + fileName;
                    System.IO.File.WriteAllBytes(pathVideo, buffer);
                }

            }
            return Ok();
        }




        [HttpGet]
        public HttpResponseMessage Profile(int school_id, int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Expression<Func<NEWS, bool>> expr = n => n.SCHOOL_ID == school_id && n.USER_ID == user_id ;
                    var numNews = entities.NEWS.Where(expr).Count();
                    int numTotalPage = (int)Math.Ceiling(numNews / 10.0);


                    var newsList = NewsView.getNewsClassList(entities, expr, 1);

                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, totalPage = numTotalPage, status = "There are not news", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, totalPage = numTotalPage, numResult = newsList.Count, page = 1, status = "Success", results = newsList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Group(int school_id, int cat_id, int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    List<NewsClass> newsList = new List<NewsClass>();
                    UserPrivateInfoClass user = new UserPrivateInfoClass(entities.USERS.FirstOrDefault(u => u.USER_ID == user_id));
                    if(user != null )
                        foreach (var group in user.groups )
                        {
                            var rowNews = entities.NEWS_ACL_GROUPS.Where(n => n.GROUP_ID == group.id).ToList();
                            foreach (var news in rowNews.Where(n => n.NEWS.NEWS_SUB_CATS.NEWS_CAT_ID == cat_id))
                                if(news.NEWS.APPROVED == 1)
                                    newsList.Add(NewsView.GetNews(news.NEWS, PrivateNewsType.Group));
                        }
                    

                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, totalPage = newsList.Count, status = "There are not news", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, totalPage = newsList.Count, numResult = newsList.Count, page = 1, status = "Success", results = newsList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Class(int school_id, int cat_id, int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    List<NewsClass> newsList = new List<NewsClass>();
                    UserPrivateInfoClass user = new UserPrivateInfoClass(user_id);
                    if (user != null)
                        foreach (var _class in user.classes)
                        {
                            var rowNews = entities.NEWS_ACL_CLASSES.Where(n => n.CLASS_ID == _class.id).ToList();
                            foreach (var news in rowNews.Where(n => n.NEWS.NEWS_SUB_CATS.NEWS_CAT_ID == cat_id))
                                if (news.NEWS.APPROVED == 1)
                                    newsList.Add(NewsView.GetNews(news.NEWS, PrivateNewsType.Class));
                        }


                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, totalPage = newsList.Count, status = "There are not news", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, totalPage = newsList.Count, numResult = newsList.Count, page = 1, status = "Success", results = newsList });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpGet]
        public HttpResponseMessage Subject(int school_id, int cat_id, int user_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    List<NewsClass> newsList = new List<NewsClass>();
                    UserPrivateInfoClass user = new UserPrivateInfoClass(user_id);
                    if (user != null)
                        foreach (var _subject in user.subjects)
                        {
                            var rowNews = entities.NEWS_ACL_SUBJECTS.Where(n => n.SUBJECT_ID == _subject.id).ToList();
                            foreach (var news in rowNews.Where(n => n.NEWS.NEWS_SUB_CATS.NEWS_CAT_ID == cat_id))
                                if (news.NEWS.APPROVED == 1)
                                    newsList.Add(NewsView.GetNews(news.NEWS, PrivateNewsType.Subject));
                        }


                    if (newsList.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, totalPage = newsList.Count, status = "There are not news", results = newsList });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, totalPage = newsList.Count, numResult = newsList.Count, page = 1, status = "Success", results = newsList });
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
                    catsList.Add(new CategoryClass(item));


                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, numResult = catsList.Count, status = "Success", results = catsList });
            }
        }

    }
}
