using System;
using System.IO;
using System.Drawing;

namespace ServerLibrary.Utils
{
    public class ImageUtils
    {
        public static string MakeThumbnail(string base64image, int rotation)
        {
            if (base64image.Length == 0)
            {
                return "";
            }
            byte[] raw = Convert.FromBase64String(base64image);
            Image img = byteArrayToImage(raw);
            ImageRotate(img, rotation);
            Image tmb = img.GetThumbnailImage(70, 70, callback, new IntPtr());
            raw = imageToByteArray(tmb);
            return Convert.ToBase64String(raw);
        }

        public static string MakeWebImage(string base64image, int rotation)
        {
            if (base64image.Length == 0)
            {
                return "";
            }
            byte[] raw = Convert.FromBase64String(base64image);
            Image img = byteArrayToImage(raw);
            ImageRotate(img, rotation);
            raw = imageToByteArray(img);
            return Convert.ToBase64String(raw);
        }

        private static void ImageRotate(Image image, int rotation)
        {
            if (rotation == 0)
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            if (rotation == 90)
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            else if (rotation == 180)
            {
                image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
        }

        private static bool callback()
        {
            return false;
        }

        private static byte[] imageToByteArray(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private static Image byteArrayToImage(byte[] raw)
        {
            MemoryStream ms = new MemoryStream(raw);
            Image img = Image.FromStream(ms);
            return img;
        }
    }
}