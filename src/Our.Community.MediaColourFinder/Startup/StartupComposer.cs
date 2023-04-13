using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;


namespace OurCommunityMediaColourFinder.Startup
{
    public class StartupComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.SetupMediaColourFinder();
         }
    }
}
