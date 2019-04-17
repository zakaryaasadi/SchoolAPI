﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SchoolAPI.Models.MD;
using SchoolAPI.Models;
using Newtonsoft.Json;
using SchoolAPI.ModelView;

namespace SchoolAPI.Views
{
    public class NewsView
    {
        public static List<NewsClass> getNewsClassList(Entities e, Expression<Func<NEWS,bool>> expr,  int page)
        {

            var newsList = e.NEWS
                .Include("NEWS_MEDIAS")
                .Include("USERS").Include("NEWS_SUB_CATS")
                .Where(expr)
                .OrderByDescending(n => n.CREATION_DATE)
                .Skip(5 * (page - 1)).Take(5)
                .ToList();

            return GetNews(e, newsList);
            
        }


        public static NewsClass GetNews(NEWS news, PrivateNewsType privteNewsType = PrivateNewsType.Public)
        {
            byte[] newsImage = null;
            string body = "";

            var _bodyMedia = news.NEWS_MEDIAS.FirstOrDefault(n => n.TYPE == 1);
            if (_bodyMedia != null)
                if(_bodyMedia.BODY != null)
                    body = HtmlToText.HtmlToPlainText(_bodyMedia.BODY);

            var _imageMedia = news.NEWS_MEDIAS.FirstOrDefault(n => n.TYPE == 2);
            if(_imageMedia != null)
                if(_imageMedia.ATTACH != null)
                    newsImage = Compression.ImageCompression(_imageMedia.ATTACH, System.Drawing.Imaging.ImageFormat.Jpeg);

           
            NewsClass newsClass = new NewsClass()
            {

                id = news.NEWS_ID,
                personName = news.USERS.FULL_NAME,
                userId = news.USERS.USER_ID,
                userName = news.USERS.USER_NAME,
                profileImage = Compression.ImageCompression(news.USERS.IMAGE, System.Drawing.Imaging.ImageFormat.Jpeg, 200, 200),

                type = news.TYPE,
                subcategory = new SubcategoryClass(news.NEWS_SUB_CATS),
                privateNewsType = privteNewsType,
                title = news.TITLE,
                headLine = news.HEADLINE,
                sharable = news.SHARABLE == 1,
                creationDate = news.CREATION_DATE,
                eventDate = news.EVENT_DATE,

                newsImage = newsImage,
                body = body
            };
            return newsClass;
        }
        public static List<NewsClass> GetNews(Entities e, List<NEWS> newsList)
        {
            List<NewsClass> newsClassList = new List<NewsClass>();

            foreach (var item in newsList)
                newsClassList.Add(GetNews(item));

            return newsClassList;
        }

        public static NEWS CreateNews(Entities e,NewsClass news)
        {
            NEWS _News = new NEWS()
            {
                NEWS_ID = e.NEWS.Max(n => n.NEWS_ID) + 1,
                USER_ID = news.userId,
                TYPE = news.type,
                NEWS_SUB_CAT_ID = news.subcategory.id,
                TITLE = news.title,
                HEADLINE = news.headLine,
                SHARABLE = news.sharable ? short.Parse("1") : short.Parse("2"),
                CREATION_DATE = DateTime.Now,
                EVENT_DATE = news.eventDate,
                VOTE_COUNT = 1,
                VOTE_RESULT = 1,
                VOTE_TYPE = 1,
                HIERARCHY_TYPE = 1,
                USERS = e.USERS.FirstOrDefault(u => u.USER_ID == news.userId),
                NEWS_SUB_CATS = e.NEWS_SUB_CATS.FirstOrDefault(n => n.NEWS_SUB_CAT_ID == news.subcategory.id),
                APPROVED = new UserPrivateInfoClass(news.userId).needApprove,
                ACCESSIBILITY = news.privateNewsType == 0 ? 1 : 2
            };


            int offset = e.NEWS_MEDIAS.Max(n => n.NEWS_MEDIA_ID) + 1;
            var _media = new NEWS_MEDIAS()
            {
                NEWS_MEDIA_ID = offset,
                NEWS_ID = _News.NEWS_ID,
                BODY = news.body,
                TYPE = 1
            };

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  