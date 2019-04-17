﻿using SchoolAPI.Models;
using SchoolAPI.Models.MD;
using SchoolAPI.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SchoolAPI.Controllers
{
    public class AttachController : ApiController
    {

        //[HttpGet]
        //public HttpResponseMessage Get(int attach_id)
        //{
        //    try
        //    {
        //        using (Entities entities = new Entities())
        //        {
        //            var _attach = entities.ATTACHMENTS
        //                .FirstOrDefault(a => a.ATTACH_ID == attach_id);

        //            if (_attach == null)
        //                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "There are not messages for you", results = null });

        //            return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 200, status = "Success", results = _attach.ATTACHMENT /*Compression.Zip(_attach.ATTACHMENT)*/ });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
        //    }
        //}

        [HttpGet]
        public HttpResponseMessage GetFile(int attach_id)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    var _attach = entities.ATTACHMENTS
                        .FirstOrDefault(a => a.ATTACH_ID == attach_id);

                    if (_attach == null)
                        return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 404, status = "There are not messages for you", results = null });

                    //Create HTTP Response.
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    byte[] bytes = _attach.ATTACHMENT;
                    string fileName = _attach.ATTACH_FILENAME;

                    //Set the Response Content.
                    response.Content = new ByteArrayContent(bytes);

                    //Set the Response Content Length.
                    response.Content.Headers.ContentLength = bytes.LongLength;

                    //Set the Content Disposition Header Value and FileName.
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));
                    return response;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Result() { statusCode = 400, status = ex.Message });
            }
        }



        private void Add(int message_id, AttachmentClass attach)
        {

            using (Entities e = new Entities())
            {
                ATTACHMENTS _attach = attach.GetAttahmentDB();
                _attach.MESSAGE_ID = message_id;
                e.ATTACHMENTS.Add(_attach);
                e.SaveChanges();
            }

        }


        [HttpPost]
        public async Task<IHttpActionResult> Add()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();

            var message_id = Request.Headers.GetValues("message_id").First();
            int messageId = int.Parse(message_id);

            var attach_no = Request.Headers.GetValues("attach_no").First();
            short attachNo = short.Parse(attach_no);

            await Request.Content.ReadAsMultipartAsync(provider);
            using (Entities e = new Entities())
            {
                MESSAGES _message = e.MESSAGES.FirstOrDefault(m => m.MESSAGE_ID == messageId );
                _message.ATTACH_NO = attachNo;
                e.SaveChanges();
            }
            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var buffer = await file.ReadAsByteArrayAsync();
                //Do whatever you want with filename and its binary data.
                Add(messageId, new AttachmentClass()
                {
                    name = filename,
                    attach = buffer
                });

            }

                

            return Ok();
        }

    }

    
}
