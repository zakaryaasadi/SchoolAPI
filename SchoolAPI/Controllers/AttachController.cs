using SchoolAPI.Models;
using SchoolAPI.Models.MD;
using SchoolAPI.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace SchoolAPI.Controllers
{
    public class AttachController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Get(int attach_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var _attach = entities.ATTACHMENTS
                        .FirstOrDefault(a => a.ATTACH_ID == attach_id);

                    if (_attach == null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "There are not messages for you", results = null });

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = Compression.Zip(_attach.ATTACHMENT) });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


        [HttpGet]
        public HttpResponseMessage Get(int attach_id, int frame)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var _attach = entities.ATTACHMENTS
                        .FirstOrDefault(a => a.ATTACH_ID == attach_id);

                    if (_attach == null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "There are not messages for you", results = null });

                    string result = Convert.ToBase64String(_attach.ATTACHMENT);

                    int size = 25 * 1024;
                    int end = size;
                    int totalFrame = (int)Math.Ceiling(result.Length / (float)size);

                    if (frame > totalFrame)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, num_result = totalFrame, status = "There is not frame: " + frame });

                    if (frame == totalFrame)
                        end = result.Length % size;

                    result = result.Substring(size * (frame - 1), end);


                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, num_result = totalFrame, status = "Success", results = result });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpPost]
        public HttpResponseMessage Add(int message_id, [FromBody] AttachmentClass attach)
        {
            try
            {
                using (Entities e = new Entities())
                {
                    ATTACHMENTS _attach = attach.GetAttahmentDB();
                    _attach.MESSAGE_ID = message_id;
                    e.ATTACHMENTS.Add(_attach);
                    e.SaveChanges();

                    AttachmentClass attachResult = new AttachmentClass(e.ATTACHMENTS.FirstOrDefault(a => a.ATTACH_ID == _attach.ATTACH_ID));
                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = attachResult });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }

        [HttpPost]
        public HttpResponseMessage Add(int message_id, [FromBody] AttachmentClass attach, int frame, bool is_last_frame = false)
        {
            try
            {
                using (Entities e = new Entities())
                {
                    ATTACHMENTS _attach;
                    bool isFirstFrame = attach.id == 0;
                    if (isFirstFrame)
                    {
                        _attach = attach.GetAttahmentDB();
                        e.ATTACHMENTS.Add(_attach);
                        e.SaveChanges();
                        attach.id = _attach.ATTACH_ID;
                    }

                    BufferDb.InsertFrame(attach, frame);

                    if (is_last_frame)
                    {
                        _attach = e.ATTACHMENTS.FirstOrDefault(a => a.ATTACH_ID == attach.id);
                        _attach.ATTACHMENT = BufferDb.GetBuffer(_attach.ATTACH_ID);
                        e.SaveChanges();
                    }
                       

                    return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success" , results = attach.id});
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }


    }


}
