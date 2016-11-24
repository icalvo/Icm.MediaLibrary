using System.Web.Http.Description;

namespace Icm.MediaLibrary.Web.Models.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiParameterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiParameterModel"/> class.
        /// </summary>
        /// <param name="apiParameterDescription">The API parameter description.</param>
        public ApiParameterModel(ApiParameterDescription apiParameterDescription)
        {
            Name = apiParameterDescription.Name;
            IsUriParameter = apiParameterDescription.Source == ApiParameterSource.FromUri;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is URI parameter].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is URI parameter]; otherwise, <c>false</c>.
        /// </value>
        public bool IsUriParameter { get; set; }
    }
}
