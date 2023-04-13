using OurCommunityMediaColourFinder.Models;

namespace OurCommunityMediaColourFinder.Interfaces;

public interface IColourService
{
    Task<IEnumerable<ImageWithColour>> GetImagesWithColour(IEnumerable<FocalPointRectangle> imageFiles);
    Task<ImageWithColour?> GetImageWithColour(FocalPointRectangle focalPointRectangle);
}
