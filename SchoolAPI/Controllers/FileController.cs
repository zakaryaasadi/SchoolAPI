using SchoolAPI.Models;
using SchoolAPI.ModelView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace SchoolAPI.Controllers
{
    public class FileController : ApiController
    {


        [HttpGet]
        public RedirectResult Thumbnail(int id)
        {
            string path = HttpContext.Current.Server.MapPath("~");
            string pathVideo = path + @"videos\" + id + ".mp4";
            var pathImage = path + @"images\" + id + ".jpg";



            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            ffMpeg.GetVideoThumbnail(pathVideo, pathImage, 5);

            var bytes = Compression.ImageCompression(File.ReadAllBytes(pathImage), System.Drawing.Imaging.ImageFormat.Jpeg, 400);

            File.WriteAllBytes(pathImage, bytes);



            var host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority); ;

            return Redirect(host + "/images/" + id + ".jpg");
        }


    }
}
