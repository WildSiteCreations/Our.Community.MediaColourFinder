using Microsoft.Extensions.Logging;
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
    private readonly ILogger<ColourSamplingMediaHandler> _logger;
    private readonly IColourService _colourService;
    private readonly MediaFileManager _mediaFileManager;

    public ColourSamplingMediaHandler(ILogger<ColourSamplingMediaHandler> logger, IColourService colourService, MediaFileManager mediaFileManager)
    {
        _logger = logger;
        _colourService = colourService;
        _mediaFileManager = mediaFileManager;
    }

    public async void Handle(MediaSavingNotification notification)
    {
        foreach (IMedia media in notification.SavedEntities)
        {
            var properties = media.GetPropertiesByEditor("wsc.mediaColourFinder");
            
            if(properties == null || !properties.Any()) continue;

            ImageWithColour? brightestColour = await ExtractColoursAsync(media) ;

            if (brightestColour is null) continue;

            foreach(var property in properties)
                TrySetProperty(media, property.Alias, System.Text.Json.JsonSerializer.Serialize(brightestColour));

        }
    }

    private async Task<ImageWithColour?> ExtractColoursAsync(IMedia media)
    {
        await using Stream stream = _mediaFileManager.GetFile(media, out _);

        FocalPointRectangle focalPoints = new FocalPointRectangle
        {
            Height = media.GetValue<int>(Umbraco.Cms.Core.Constants.Conventions.Media.Height),
            Width = media.GetValue<int>(Umbraco.Cms.Core.Constants.Conventions.Media.Width),
            Stream = stream
        };

        // TODO: We need to get this dynamically from local or from global crops!
        var x = 0.5m;
        var y = 0.5m;

        focalPoints.Left = x;
        focalPoints.Top = y;

        return await _colourService.GetImageWithColour(focalPoints);
    }

    private static void TrySetProperty(IMedia media, string propertyAlias, object value)
    {
        media.SetValue(propertyAlias, value);
        
    }
}

