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
        private readonly ILogg

        public MediaComponent(IMediaTypeService mediaTypeService, IShortStringHelper shortStringHelper, IDataTypeService dataTypeService)
        {
            _mediaTypeService = mediaTypeService;
            _shortStringHelper = shortStringHelper;
            _dataTypeService = dataTypeService;
        }

        public void Initialize()
        {

            try
            {
                var mediaType = _mediaTypeService.Get("Image");
                var colorPickerDataType = _dataTypeService.GetByEditorAlias("Umbraco.TextBox").First();

                var propertyType = new PropertyType(_shortStringHelper, colorPickerDataType)
                {
                    Name = "Color",
                    Alias = "colourFound",
                    Description = "Property Description",


                    Mandatory = false


                };


                mediaType.AddPropertyType(propertyType, "image");

                // Save the changes to the database
                _mediaTypeService.Save(mediaType);
            }
            catch(Exception ex)
            {
                
            }
            // Get the media type you want to modify
          
        }

        public void Terminate() { }
    }
}

