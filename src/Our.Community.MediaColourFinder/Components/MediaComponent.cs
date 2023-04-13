using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Community.MediaColourFinder.Common;


namespace OurCommunityMediaColourFinder.Components;

public class MediaComponent : IComponent
{
    private readonly IMediaTypeService _mediaTypeService;
    private readonly IShortStringHelper _shortStringHelper;
    private readonly IDataTypeService _dataTypeService;
    private readonly ILogger<MediaComponent> _logger;

    public MediaComponent(IMediaTypeService mediaTypeService, IShortStringHelper shortStringHelper,
        IDataTypeService dataTypeService, ILogger<MediaComponent> logger)
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
            IMediaType? mediaType = _mediaTypeService.Get("Image");
         
            var colorPicker =  _dataTypeService
                .GetByEditorAlias(Umbraco.Cms.Core.Constants.PropertyEditors.Aliases.ColorPickerEyeDropper)
                .FirstOrDefault();


            //TODO: If colorpicker is null - create one


            PropertyType propertyType = new(_shortStringHelper, colorPicker)
            {
                Name = "Brightest Colour Found",
                Alias = ApplicationConstants.BrightestColourFoundPropertyAlias,
                Description = "This is the brightest colour found within the focal area of this image",
                Mandatory = false,
            };

            PropertyType oppositeColour = new(_shortStringHelper, colorPicker)
            {
                Name = "Opposite Colour Found",
                Alias = ApplicationConstants.OppositeColourPropertyAlias,
                Description = "This is the opposite colour",
                Mandatory = false
            };

            PropertyType whiteOrBlack = new(_shortStringHelper, colorPicker)
            {
                Name = "White or Black contrast",
                Alias = ApplicationConstants.WhiteOrBlackPropertyAlias,
                Description = "This is the best colour for readability, will be #000000 or #FFFFFF",
                Mandatory = false,
            };


            mediaType.AddPropertyType(propertyType, "image");
            mediaType.AddPropertyType(oppositeColour, "image");
            mediaType.AddPropertyType(whiteOrBlack, "image");

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
