using OurCommunityMediaColourFinder.Models;

namespace OurCommunityMediaColourFinder.Interfaces;

public interface IColourService
{
    IEnumerable<ImageWithColour> GetImagesWithColour(IEnumerable<FocalPointRectangle> imageFiles);
    ImageWithColour? GetImageWithColour(FocalPointRectangle focalPointRectangle);
}
