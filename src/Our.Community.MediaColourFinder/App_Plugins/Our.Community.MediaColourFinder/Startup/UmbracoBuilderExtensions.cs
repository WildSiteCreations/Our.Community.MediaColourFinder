using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;
using System.Linq;
using OurCommunityMediaColourFinder.Services;

namespace OurCommunityMediaColourFinder.Startup
{
    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder SetupMediaColourFinder(this IUmbracoBuilder builder)
        {
            builder.Services.AddUnique<IColourService, ColourService>();
          

            return builder;
        }

     
    }
}
