using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Umbraco.Community.Our.Community.MediaColourFinder
{
    internal class MediaColourFinderComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.ManifestFilters().Append<MediaColourFinderManifestFilter>();
        }
    }
}
