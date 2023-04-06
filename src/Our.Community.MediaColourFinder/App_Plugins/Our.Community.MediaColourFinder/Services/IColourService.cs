using MediaColourFinder.Models;

namespace MediaColourFinder.Services;

public interface IColourService
{
    Task<IEnumerable<ImageWithColour>> GetImagesWithColour(IEnumerable<FocalPointRectangle> imageFiles);
}
