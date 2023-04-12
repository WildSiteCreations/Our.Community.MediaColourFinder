using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;


namespace OurCommunityMediaColourFinder.Components
{
    public class MediaComponent : IComponent
    {
        private readonly IMediaTypeService _mediaTypeService;
        private readonly IShortStringHelper _shortStringHelper;
        private readonly IDataTypeService _dataTypeService;
        private readonly ILogger<MediaComponent> _logger;

        public MediaComponent(IMediaTypeService mediaTypeService, IShortStringHelper shortStringHelper, IDataTypeService dataTypeService, ILogger<MediaComponent> logger)
        {
            _mediaTypeService = mediaTypeService;
            _shortStringHelper = shortStringHelper;
            _dataTypeService = dataTypeService;
            _logger = logger;
        }

        public void Initialize()
        {

            try
            {
                var mediaType = _mediaTypeService.Get("Image");
                var colorPickerDataType = _dataTypeService.GetByEditorAlias("Umbraco.TextBox").First();

                var propertyType = new PropertyType(_shortStringHelper, colorPickerDataType)
                {
                    Name = "Brightest Colour Found",
                    Alias = "brightestColourFound",
                    Description = "This is the brightest colour found within the focal area of this image",
                    Mandatory = false


                };


                mediaType.AddPropertyType(propertyType, "image");

                // Save the changes to the database
                _mediaTypeService.Save(mediaType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error within Media Component", ex);
            }


        }

        public void Terminate() { }
    }
}

