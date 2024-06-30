using WSC.MediaColourFinder.Core.Models;

namespace WSC.MediaColourFinder.Core.Interfaces
{
	public interface IColourService
	{
		IEnumerable<ImageWithColour> GetImagesWithColour(IEnumerable<FocalPointRectangle> imageFiles);
		ImageWithColour? GetImageWithColour(FocalPointRectangle focalPointRectangle);
	}
}
