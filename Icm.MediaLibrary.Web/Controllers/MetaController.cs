using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Icm.MediaLibrary.Web.Models.Meta;

namespace Icm.MediaLibrary.Web.Controllers
{
    /// <summary>
    /// Metadata controller
    /// </summary>
    public class MetaController : ApiController
    {
        /// <summary>
        /// Gets the metadata for the REST service methods
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ApiMethodModel>))]
        public IHttpActionResult Get()
        {
            IApiExplorer apiExplorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();
            IEnumerable<ApiMethodModel> apiMethods = apiExplorer.ApiDescriptions.Select(ad => new ApiMethodModel(ad)).ToList();
            return Ok(apiMethods);
        }
    }
}