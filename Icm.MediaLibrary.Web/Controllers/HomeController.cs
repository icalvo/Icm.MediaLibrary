using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Icm.MediaLibrary.Domain;
using Icm.MediaLibrary.Infrastructure;
using Icm.MediaLibrary.Web.Models;
using Lib.Web.Mvc;

namespace Icm.MediaLibrary.Web.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OceansClip(string filename)
        {
            FileInfo oceansClipInfo = new FileInfo(filename);

            string extension = oceansClipInfo.Extension.Remove(0, 1);
            string oceansClipMimeType;

            switch (extension)
            {
                case "mp4":
                    oceansClipMimeType = "video/mp4";
                    break;
                case "webm":
                    oceansClipMimeType = "video/webm";
                    break;
                case "ogg":
                    oceansClipMimeType = "video/ogg";
                    break;
                default:
                    return null;
            }

            return new RangeFilePathResult(oceansClipMimeType, oceansClipInfo.FullName, oceansClipInfo.LastWriteTimeUtc, oceansClipInfo.Length);
        }

        /// <summary>
        /// Datas the table.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public JsonResult DataTable(DataTablesRequest request)
        {

            IMediaRepository repository = new DbMediaRepository(new EntityFrameworkSexContext());

            var videos = repository.GetVideos();

//            return Json(
//                request.Response(
//                    videos.AsQueryable(),
//                    new Field<Video, string>(video => video.Hash, (video, search) => video.Hash.Contains(search)),
//                    new Field<Video, string>(video => video.FileName, (video, search) => video.FileName.Contains(search), (videoFileName) => string.Format(@"
//<video id=""videoBox"" class=""video-js vjs-default-skin"" controls preload=""none"" width=""640"" height=""264"" data-setup=""{{}}"">
//   <source src=""{0}"" type='video/mp4' />
//</video>", Url.Action("OceansClip", new { filename = videoFileName }))),
//                    new Field<Video, TimeSpan>(video => video.Duration, (IQueryable<Video> items, string search) =>
//                    {
//                        int maxDuration = int.Parse(search);
//                        return items.Where(video => DbFunctions.DiffMinutes(TimeSpan.Zero, video.Duration) <= maxDuration);
//                    }),
//                    new Field<Video, string>(video => video.NormalizedTags, (video, search) => video.NormalizedTags.Contains(search))),
//                JsonRequestBehavior.AllowGet);
            return Json(
                request.Response(
                    videos.AsQueryable(),
                    new Field<Video, string>(
                        video => video.FileName, 
                        (video, search) => video.FileName.Contains(search)),
                    new Field<Video, TimeSpan>(
                        video => video.Duration, 
                        (IQueryable<Video> items, string search) =>
                        {
                            try
                            {
                                var split = search.Split('-');
                                if (split.Count() == 1)
                                {
                                    int maxDuration = int.Parse(search);
                                    return items.Where(video => DbFunctions.DiffMinutes(TimeSpan.Zero, video.Duration) <= maxDuration);
                                }

                                if (split.Count() == 2)
                                {
                                    int minDuration = int.Parse(split[0]);
                                    int maxDuration = int.Parse(split[1]) + 1;
                                    return items.Where(video => DbFunctions.DiffMinutes(TimeSpan.Zero, video.Duration) <= maxDuration && DbFunctions.DiffMinutes(TimeSpan.Zero, video.Duration) >= minDuration);
                                }
                            }
                            catch (Exception)
                            {
                            }
                            return items;
                        },
                        duration => duration.ToString(@"hh\:mm\:ss")),
                    new Field<Video, string>(
                        video => video.NormalizedTags, 
                        (video, search) => video.NormalizedTags.Contains(search))),
                JsonRequestBehavior.AllowGet);
        }
    }
}
