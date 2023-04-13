using OurCommunityMediaColourFinder.Interfaces;
using OurCommunityMediaColourFinder.Models;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace OurCommunityMediaColourFinder.Services;

public class ColourService : IColourService
{
    public async Task<ImageWithColour?> GetImageWithColour(FocalPointRectangle focalPointRectangle)
    {
        return (await GetImagesWithColour(new[] { focalPointRectangle })).FirstOrDefault();
    }

    public async Task<IEnumerable<ImageWithColour>> GetImagesWithColour(IEnumerable<FocalPointRectangle> imageFiles)
    {
        // Get the dominant color of each image
        var imagesWithColour = new List<ImageWithColour>();
        foreach (var image in imageFiles)
        {
            var colours = await GetDominantColor(image);

            var imageWithColour = new ImageWithColour
            {
                // Path = image.Image,
                // Brightest = colours.ToHex(),
                Opposite = InvertColorAndConvertToHex(colours.ToHex(), false),
                BrightestContrast = InvertColorAndConvertToHex(colours.ToHex(), true),
                Average = $"#{colours.ToHex()[..6]}",
            };

            imagesWithColour.Add(imageWithColour);
        }

        return imagesWithColour;
    }

    private static async Task<Rgba32> GetDominantColor(FocalPointRectangle focusArea, int resizeWidth = 16,
        int resizeHeight = 16)
    {
        // Download the image
        // var imageStream = await DownloadImageAsync("https://localhost:44302" + imagePath);


        // Load the image with ImageSharp
        using var image = Image.Load<Rgba32>(focusArea.Stream);

        // Crop the image to the focus area
        var rectangle = focusArea.GetRectangle();
        image.Mutate(x => x.Crop(rectangle));

        // Resize the image to reduce the number of pixels
        // This could be uncommented and not use resizing in the focusArea.GetRectangle() method
        // image.Mutate(x => x.Resize(new ResizeOptions
        // {
        //     Size = new Size(resizeWidth, resizeHeight),
        //     Sampler = KnownResamplers.NearestNeighbor
        // }));

        // Calculate the average color
        long rSum = 0, gSum = 0, bSum = 0;
        var totalPixels = resizeWidth * resizeHeight;

        for (var y = 0; y < resizeHeight; y++)
        {
            for (var x = 0; x < resizeWidth; x++)
            {
                var pixel = image[x, y];
                rSum += pixel.R;
                gSum += pixel.G;
                bSum += pixel.B;
            }
        }

        return new Rgba32(
            (byte)(rSum / totalPixels),
            (byte)(gSum / totalPixels),
            (byte)(bSum / totalPixels));
    }

    public static string InvertColorAndConvertToHex(string hexValue, bool isBlackAndWhite)
    {
        if (hexValue.IndexOf('#') == 0)
        {
            hexValue = hexValue.Substring(1);
        }
        // convert 3-digit hex to 6-digits.
        if (hexValue.Length == 3)
        {
            hexValue = hexValue[0].ToString() + hexValue[0].ToString() + hexValue[1].ToString() + hexValue[1].ToString() + hexValue[2].ToString() + hexValue[2].ToString();
        }

        if (hexValue.Length == 8)
        {
            // trim the last two characters as this is the "alpha" value
            hexValue = hexValue[..6];
        }

        if (hexValue.Length != 6)
        {
            throw new Exception("Invalid HEX color.");
        }
        var r = Convert.ToInt32(hexValue.Substring(0, 2), 16);
        var g = Convert.ToInt32(hexValue.Substring(2, 2), 16);
        var b = Convert.ToInt32(hexValue.Substring(4, 2), 16);
        if (isBlackAndWhite)
        {
            // https://stackoverflow.com/a/3943023/112731
            return (r * 0.299 + g * 0.587 + b * 0.114) > 186
                ? "#000000"
                : "#FFFFFF";
        }
        // invert color components
        r = (255 - r);
        g = (255 - g);
        b = (255 - b);
        // convert to hex
        var hexR = r.ToString("X2");
        var hexG = g.ToString("X2");
        var hexB = b.ToString("X2");
        // pad each with zeros and return
        return "#" + PadZero(hexR) + PadZero(hexG) + PadZero(hexB);
    }

    public static string PadZero(string str, int len = 2)
    {
        var zeros = new string('0', len);
        return (zeros + str).Substring(str.Length);
    }


}
