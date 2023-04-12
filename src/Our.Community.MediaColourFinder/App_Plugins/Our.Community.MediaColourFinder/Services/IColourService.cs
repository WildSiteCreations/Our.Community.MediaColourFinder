using OurCommunityMediaColourFinder.Models;

namespace OurCommunityMediaColourFinder.Services;

public interface IColourService
{
    Task<IEnumerable<ImageWithColour>> GetImagesWithColour(IEnumerable<FocalPointRectangle> imageFiles);
}
