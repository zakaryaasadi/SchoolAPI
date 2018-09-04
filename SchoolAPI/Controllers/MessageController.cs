using SchoolAPI.Models;
using SchoolAPI.Models.MD;
using SchoolAPI.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolAPI.Controllers
{
    public class MessageController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(int user_id, int page = 1)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    List<MessageClass> messages = new List<MessageClass>();

                    var _messagesTotal = entities.MESSAGE_RECIPIENTS
                        .Include("MESSAGES")
                        .Where(m => m.USER_ID == user_id)
                        .OrderByDescending(m => m.MESSAGE_ID);

                    var _messages = _messagesTotal.Skip(5 * (page - 1)).Take(5);

                    foreach (var item in _messages)
                        messages.Add(new MessageClass(item.MESSAGES));



                    int numTotalPage = (int)Math.Ceiling( _messagesTotal.Count() / 5.0);

                    if (page > numTotalPage)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, total_page = numTotalPage, status = "There is not page: " + page });

                    if (messages.Count == 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, total_page = numTotalPage, status = "There are not messages for you", results = messages });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, total_page = numTotalPage, num_result = messages.Count, page = page, status = "Success", results = messages });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody] MessageClass message, [FromBody] List<UserPublicInfoClass> users)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    MESSAGES _message = message.GetMessageDB();

                    foreach (var user in users)
                    {
                        MESSAGE_RECIPIENTS _mr = new MESSAGE_RECIPIENTS()
                        {
                            MESSAGE_RECIPIENT_ID = entities.MESSAGE_RECIPIENTS.Max(mr => mr.MESSAGE_RECIPIENT_ID) + 1,
                            MESSAGE_ID = _message.MESSAGE_ID,
                            USER_ID = user.id
                        };
                        entities.MESSAGE_RECIPIENTS.Add(_mr);
                    }

                    entities.SaveChanges();

                    MessageClass messageResult = new MessageClass( entities.MESSAGES.FirstOrDefault(n => n.MESSAGE_ID == _message.MESSAGE_ID) );
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = messageResult });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody] MessageClass message)
        {

            using (Entities entities = new Entities())
            {
                var _users = entities.GROUPS.FirstOrDefault(g => g.GROUP_ID == message.group.id).GROUP_MEMBERS;

                List<UserPublicInfoClass> users = new List<UserPublicInfoClass>();
                foreach (var item in _users)
                    users.Add(new UserPublicInfoClass(item.USERS));

                return Add(message, users);
            }
        }
    }
}
