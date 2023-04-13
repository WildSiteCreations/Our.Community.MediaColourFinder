using OurCommunityMediaColourFinder.Interfaces;
using OurCommunityMediaColourFinder.Models;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Community.MediaColourFinder.Common;
using Umbraco.Extensions;

namespace Umbraco.Community.Our.Community.MediaColourFinder.Handlers;

/// <summary>
/// This is called when a media item is being saved, we can use this to extract the average colour from the image using
/// its focal point.
/// </summary>
public class ColourSamplingMediaHandler : INotificationHandler<MediaSavingNotification>
{
    private readonly IColourService _colourService;
    private readonly MediaFileManager _mediaFileManager;

    public ColourSamplingMediaHandler(IColourService colourService, MediaFileManager mediaFileManager)
    {
        _colourService = colourService;
        _mediaFileManager = mediaFileManager;
    }

    public async void Handle(MediaSavingNotification notification)
    {
        foreach (IMedia media in notification.SavedEntities)
        {
            var hasProperty = media.Properties
                .TryGetValue(ApplicationConstants.BrightestColourFoundPropertyAlias,
                    out IProperty? colourFoundProperty);

            if (!hasProperty)
            {
                continue;
            }

            await using Stream stream = _mediaFileManager.GetFile(media, out _);

            FocalPointRectangle focalPoints = new FocalPointRectangle
            {
                Height = media.GetValue<int>(Umbraco.Cms.Core.Constants.Conventions.Media.Height),
                Width = media.GetValue<int>(Umbraco.Cms.Core.Constants.Conventions.Media.Width),
                // Image = media.GetUrl("umbracoFile", _generatorCollection)
                Stream = stream
            };

            // TODO: We need to get this dynamically from local or from global crops!
            var x = 0.5m;
            var y = 0.5m;

            focalPoints.Left = x;
            focalPoints.Top = y;


            ImageWithColour? brightestColour = await _colourService.GetImageWithColour(focalPoints);

            if (brightestColour is null)
            {
                continue;
            }

            TrySetProperty(media, ApplicationConstants.OppositeColourPropertyAlias, brightestColour.Opposite);
            TrySetProperty(media, ApplicationConstants.WhiteOrBlackPropertyAlias, brightestColour.BrightestContrast);
            TrySetProperty(media, ApplicationConstants.BrightestColourFoundPropertyAlias, brightestColour.Average);

        }
    }

    private static void TrySetProperty(IMedia media, string propertyAlias, object value)
    {
        if (media?.Properties != null && media.Properties.TryGetValue(propertyAlias, out IProperty? property))
        {
            property.SetValue(value);
        }
    }
}

