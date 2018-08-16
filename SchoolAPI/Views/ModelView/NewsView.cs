using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SchoolAPI.Models.MD;
using SchoolAPI.Models;
using Newtonsoft.Json;

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

                foreach (var media in news.NEWS_MEDIAS)
                {
                    if (media.ATTACH != null)
                        newsImage = media.ATTACH;
                    if (media.BODY != null)
                        body = media.BODY;
                }


                NewsClass newsClass = new NewsClass()
                {

                    id = news.NEWS_ID,
                    personName = news.USERS.FULL_NAME,
                    userId = news.USERS.USER_ID,
                    userName = news.USERS.USER_NAME,
                    profileImage = news.USERS.IMAGE,
                   
                    type = news.TYPE,
                    subcategory = new SubcategoryClass() { id = news.NEWS_SUB_CATS.NEWS_SUB_CAT_ID, title = news.NEWS_SUB_CATS.TITLE },
                    privateNewsType = privteNewsType,
                    title = news.TITLE,
                    headLine = news.HEADLINE,
                    sharable = news.SHARABLE == 0 ? false : true,
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

        public static List<VotingClass> getVotingClassList(Entities e, Expression<Func<NEWS, bool>> expr, int page, int userId = 0)
        {
            List<NewsClass> newsList = getNewsClassList(e, expr, page);
            List<VotingClass> votings = new List<VotingClass>();

            foreach (var item in newsList)
            {
                var serializedParent = JsonConvert.SerializeObject(item);
                VotingClass votingClass = JsonConvert.DeserializeObject<VotingClass>(serializedParent);

                NEWS _NEWS = e.NEWS.FirstOrDefault(n => n.NEWS_ID == item.id);

                votingClass.voteType = _NEWS.VOTE_TYPE;
                votingClass.voteResult = _NEWS.VOTE_RESULT;
                votingClass.voteCount = _NEWS.VOTE_COUNT == 1 ? true : false;
                votingClass.expierDate = _NEWS.EXPIRATION_DATE;
                votingClass.choices = getChoices(e, item.id, userId);

                
                foreach (var choice in votingClass.choices)
                    votingClass.totalVotes += choice.voteCount;

                votings.Add(votingClass);
            }

            return votings;
        }


        private static List<ChoiceClass> getChoices(Entities e, int newsId, int userId)
        {
            List<ChoiceClass> choices = new List<ChoiceClass>();
            List<VOTING_CHOICES> VC = e.VOTING_CHOICES.Where(v => v.NEWS_ID == newsId).ToList();

            foreach (var item in VC)
            {
                choices.Add(new ChoiceClass()
                {
                    id = item.VOTING_CHOICE_ID,
                    title = item.CHOICE,
                    voteCount = e.USER_VOTES.Where(uv => uv.VOTING_CHOICE_ID == item.VOTING_CHOICE_ID).ToList().Count(),
                    isChoiced = e.USER_VOTES.FirstOrDefault(uv => uv.USER_ID == userId && uv.VOTING_CHOICE_ID == item.VOTING_CHOICE_ID) == null ? false : true,
                    newsId = newsId
                });

            }
            return choices;
        }

    }
}