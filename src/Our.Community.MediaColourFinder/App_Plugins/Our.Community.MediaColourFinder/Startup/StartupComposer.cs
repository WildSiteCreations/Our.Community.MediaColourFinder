using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using OurCommunityMediaColourFinder.Components;


namespace OurCommunityMediaColourFinder.Startup
{
    public class StartupComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.SetupMediaColourFinder();
            builder.Components().Append<MediaComponent>();
         }
    }
}
