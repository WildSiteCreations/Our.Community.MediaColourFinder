using Umbraco.Cms.Core.Manifest;

namespace Umbraco.Community.Our.Community.MediaColourFinder
{
    internal class Our.Community.MediaColourFinderManifestFilter : IManifestFilter
    {
        public void Filter(List<PackageManifest> manifests)
        {
            var assembly = typeof(Our.Community.MediaColourFinderManifestFilter).Assembly;

            manifests.Add(new PackageManifest
            {
                PackageName = "Our.Community.MediaColourFinder",
                Version = assembly.GetName()?.Version?.ToString(3) ?? "0.1.0",
                AllowPackageTelemetry = true
            });
        }
    }
}
