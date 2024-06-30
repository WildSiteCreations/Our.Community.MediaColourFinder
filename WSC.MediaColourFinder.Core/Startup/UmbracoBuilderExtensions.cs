using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;
using WSC.MediaColourFinder.Core.Handlers;
using WSC.MediaColourFinder.Core.Interfaces;
using WSC.MediaColourFinder.Core.Services;

namespace WSC.MediaColourFinder.Core.Startup
{
	internal static class UmbracoBuilderExtensions
	{
		public static IUmbracoBuilder SetupMediaColourFinder(this IUmbracoBuilder builder)
		{
			builder.Services.AddUnique<IColourService, ColourService>();

			builder.AddNotificationHandler<MediaSavingNotification, ColourSamplingMediaHandler>();
			
			return builder;
		}
	}
}
