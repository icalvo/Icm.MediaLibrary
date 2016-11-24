using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace Icm.MediaLibrary.Web.Models.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiMethodModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiMethodModel"/> class.
        /// </summary>
        /// <param name="apiDescription">The API description.</param>
        public ApiMethodModel(ApiDescription apiDescription)
        {
            Method = apiDescription.HttpMethod.Method;
            Url = apiDescription.RelativePath;
            ControllerName = apiDescription.ActionDescriptor.ControllerDescriptor.ControllerName;
            ActionName = apiDescription.ActionDescriptor.ActionName;
            Parameters = apiDescription.ParameterDescriptions.Select(pd => new ApiParameterModel(pd));
        }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        /// <value>
        /// The name of the controller.
        /// </value>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public IEnumerable<ApiParameterModel> Parameters { get; set; }
    }
}
