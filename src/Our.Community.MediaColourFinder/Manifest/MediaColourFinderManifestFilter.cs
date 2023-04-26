using Umbraco.Cms.Core.Manifest;

namespace Umbraco.Community.MediaColourFinder.Manifest
{
    internal class MediaColourFinderManifestFilter : IManifestFilter
    {
       
        public void Filter(List<PackageManifest> manifests)
        {
            manifests.Add(new PackageManifest
            {
                PackageName = "Our.Community.MediaColourFinder",
                Version = typeof(MediaColourFinderManifestFilter).Assembly.GetName().Version.ToString(3),
                Scripts = new[]
                {
                    $"/App_Plugins/Our.Community.MediaColourFinder/mediaColourFinder.js"
                },
                Stylesheets = new[]
                {
                    $"/App_Plugins/Our.Community.MediaColourFinder/mediaColourFinder.css"
                }
            });
        }
    }
}
