using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Icm.MediaLibrary.Domain;
using Icm.MediaLibrary.Infrastructure;
using Icm.MediaLibrary.Web.Models;

namespace Icm.MediaLibrary.Web.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        public JsonResult DataTable(DataTablesRequest request)
        {

            IMediaRepository repository = new DbMediaRepository(new EntityFrameworkSexContext());

            var videos = repository.GetVideos();

            return Json(
                request.Response(
                    videos.AsQueryable(),
                    new Field<Video, string>(video => video.Hash, (video, search) => video.Hash.Contains(search)),
                    new Field<Video, string>(video => video.FileName),
                    new Field<Video, TimeSpan>(video => video.Duration)
                    ), 
                JsonRequestBehavior.AllowGet);
        }
    }
}
