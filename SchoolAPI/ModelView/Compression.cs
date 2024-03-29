﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace SchoolAPI.ModelView
{
    public class Compression
    {
        private static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static byte[] Zip(string str)
        {
            var bytes = Convert.FromBase64String(str);
            return Zip(bytes);
        }

        public static byte[] Zip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Unzip(string str)
        {
            return Unzip(Convert.FromBase64String(str));
        }

        public static byte[] ImageCompression(byte[] image, ImageFormat format)
        {
            if (image == null)
                return null;
            Image img = Base64ToImage(Convert.ToBase64String(image));
            return ImageCompression(image, format, img.Height, img.Width);

        }
        public static byte[] ImageCompression(byte[] image, ImageFormat format,int height, int width)
        {
            if (image == null)
                return null;
            Image img = Base64ToImage(Convert.ToBase64String(image));
            img = VaryQualityLevel(img, format, height, width);
            string base64 = ImageToBase64(img, format);
            return Convert.FromBase64String(base64);

        }


        public static byte[] ImageCompression(byte[] image, ImageFormat format, int maxPixelImage)
        {
            if (image == null)
                return null;
            Image img = Base64ToImage(Convert.ToBase64String(image));          


            if (img.Width > img.Height)
            {
                if (img.Width < maxPixelImage)
                    maxPixelImage = img.Width;
                float percent = img.Height / float.Parse(img.Width.ToString());
                img = VaryQualityLevel(img, format, (int)Math.Round(maxPixelImage * percent), maxPixelImage);
            }
            else
            {
                if (img.Height < maxPixelImage)
                    maxPixelImage = img.Height;
                float percent = img.Width / float.Parse(img.Height.ToString());
                img = VaryQualityLevel(img, format, maxPixelImage, (int)Math.Round(maxPixelImage * percent));
            }

            string base64 = ImageToBase64(img, format);
            return Convert.FromBase64String(base64);

        }




        private static string ImageToBase64(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to base 64 string
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        private static Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        private static Image VaryQualityLevel(Image image, ImageFormat format, int height, int width)
        {
            Bitmap bmp1 = new Bitmap(image, width, height);
            ImageCodecInfo jgpEncoder = GetEncoder(format);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            int myFileName = new Random().Next();
            string path = HostingEnvironment.MapPath(@"~/App_Data/" + myFileName.ToString() ) + "." + format.ToString();
            bmp1.Save(path, jgpEncoder,myEncoderParameters);
            return bmp1;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}