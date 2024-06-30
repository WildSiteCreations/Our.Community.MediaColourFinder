using Newtonsoft.Json;

namespace WSC.MediaColourFinder.Core.Models
{
	public class ImageDataProxy
	{
		[JsonProperty("src")]
		public string Source { get; set; }

		[JsonProperty("focalPoint")]
		public FocalPoint? FocalPoint { get; set; }
	}

	public class FocalPoint
	{
		[JsonProperty("left")]
		public double Left { get; set; }

		[JsonProperty("top")]
		public double Top { get; set; }
	}
}
