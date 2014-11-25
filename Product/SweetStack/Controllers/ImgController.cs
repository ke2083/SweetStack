using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SweetStack.Controllers
{
    public class ImgController : Controller
    {

        public ActionResult Thumbnail(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            var height = 250;
            try
            {
                var f = System.IO.File.ReadAllBytes(Server.MapPath(path));
                FileContentResult finishedThumbnail = null;
                var stream = new MemoryStream(f, 0, f.Length);
                using (var image = System.Drawing.Image.FromStream(stream))
                {
                    var sizeRatio = (decimal)image.Width / image.Height;
                    var imgWidth = 0;
                    imgWidth = (int)(height * sizeRatio);
                    using (var thumbNail = image.GetThumbnailImage(imgWidth, height, () => { return true; }, System.IntPtr.Zero))
                    {
                        var saveStream = new MemoryStream();
                        thumbNail.Save(saveStream, System.Drawing.Imaging.ImageFormat.Png);
                        saveStream.Position = 0;
                        var thumbnailData = new byte[saveStream.Length];
                        saveStream.Read(thumbnailData, 0, (int)saveStream.Length);
                        finishedThumbnail = File(thumbnailData, "image/png");
                    }
                }

                return finishedThumbnail;
            }
            catch (System.IO.FileNotFoundException)
            {
                return null;
            }
        }

    }
}
