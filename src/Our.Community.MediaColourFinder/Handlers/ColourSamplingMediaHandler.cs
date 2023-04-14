using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OurCommunityMediaColourFinder.Interfaces;
using OurCommunityMediaColourFinder.Models;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Community.MediaColourFinder.Models;
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

    public ColourSamplingMediaHandler(ILogger<ColourSamplingMediaHandler> logger, IColourService colourService,
        MediaFileManager mediaFileManager)
    {
        _logger = logger;
        _colourService = colourService;
        _mediaFileManager = mediaFileManager;
    }

    public async void Handle(MediaSavingNotification notification)
    {
        foreach (IMedia media in notification.SavedEntities)
        {
            IEnumerable<IProperty> properties = media
                .GetPropertiesByEditor("wsc.mediaColourFinder")
                .ToList(); // ToList() is important here, otherwise the enumeration will be executed multiple times

            if (!properties.Any())
            {
                continue;
            }

            ImageWithColour? imagesWithColour = await ExtractColoursAsync(media);

            if (imagesWithColour is null)
            {
                continue;
            }

            foreach (IProperty property in properties)
            {
                media.SetValue(property.Alias, System.Text.Json.JsonSerializer.Serialize(imagesWithColour));
            }
        }
    }

    private async Task<ImageWithColour?> ExtractColoursAsync(IMedia media)
    {
        await using Stream stream = _mediaFileManager.GetFile(media, out _);

        var mediaWithCrops = media.GetValue("umbracoFile")?.ToString();
        if (mediaWithCrops == null)
        {
            return null;
        }

        ImageDataProxy? imageDataProxy = JsonConvert.DeserializeObject<ImageDataProxy>(mediaWithCrops);

        if (imageDataProxy == null)
        {
            return null;
        }

        FocalPointRectangle focalPoints = new()
        {
            Height = media.GetValue<int>(Cms.Core.Constants.Conventions.Media.Height),
            Width = media.GetValue<int>(Cms.Core.Constants.Conventions.Media.Width),
            Stream = stream,
        };
        if (imageDataProxy.FocalPoint != null)
        {
            focalPoints.Left = (decimal)imageDataProxy.FocalPoint.Left;
            focalPoints.Top = (decimal)imageDataProxy.FocalPoint.Top;
        }
        else
        {
            // no focal points have been set, this can happen when a user first uploads.
            // That's okay, it's always defaulting to these values anyway.
            focalPoints.Left = 0.5m;
            focalPoints.Top = 0.5m;
        }

        return _colourService.GetImageWithColour(focalPoints);
    }
}
