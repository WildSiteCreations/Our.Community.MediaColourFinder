using SixLabors.ImageSharp;

namespace OurCommunityMediaColourFinder.Models;

public class FocalPointRectangle
{
    public decimal Left { get; set; }

    public decimal Top { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }
    public string? Image { get; set; }

    /// <summary>
    /// We calculate the rectangle based on the focal point and the image size.
    /// </summary>
    public Rectangle GetRectangle()
    {
        var rectangleSize = 20; // This can probably be adjusted to be something a little bit nicer.
        var x = (int) (Left * Width);
        var y = (int) (Top * Height);
        return new Rectangle(x, y, rectangleSize, rectangleSize);
    }
}
