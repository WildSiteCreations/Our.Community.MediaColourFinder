using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Community.MediaColourFinder.Common;
using Umbraco.Extensions;

namespace OurCommunityMediaColourFinder.PropertyValueConverters
{
    public class MediaColourFinderValueConverter : PropertyValueConverterBase
    {
        private readonly ILogger<MediaColourFinderValueConverter> _logger;

        public MediaColourFinderValueConverter(ILogger<MediaColourFinderValueConverter> logger)
        {
            _logger = logger;
        }
        public override bool IsConverter(IPublishedPropertyType propertyType) =>
            propertyType.EditorAlias == ApplicationConstants.PropertyTypeAlias;

        public override Type GetPropertyValueType(IPublishedPropertyType propertyType)
            => typeof(Models.ImageWithColour);

        public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType)
            => PropertyCacheLevel.Element;

        public override object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview)
        {
            if (source == null)
            {
                return null;
            }

            var sourceString = source.ToString()!;

            if (sourceString.DetectIsJson())
            {
                try
                {
                    var obj = System.Text.Json.JsonSerializer.Deserialize<Models.ImageWithColour>(sourceString);
                    return obj;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Could not parse the string '{JsonString}' to a json object", sourceString);
                }
            }

            return sourceString;
        }
    }
    
}