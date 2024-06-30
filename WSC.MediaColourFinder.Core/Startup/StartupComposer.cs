using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;


namespace WSC.MediaColourFinder.Core.Startup
{
	public class StartupComposer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.SetupMediaColourFinder();
		}
	}
}
