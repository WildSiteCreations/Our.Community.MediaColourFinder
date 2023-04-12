using OurCommunityMediaColourFinder.Models;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace OurCommunityMediaColourFinder.Services;

public class ColourService : IColourService
{
    public async Task<IEnumerable<ImageWithColour>> GetImagesWithColour(IEnumerable<FocalPointRectangle> imageFiles)
    {
        // Get the dominant color of each image
        var imagesWithColour = new List<ImageWithColour>();
        foreach (var image in imageFiles)
        {
            if (image.Image == null) 
                continue;
            
            var colours = await GetDominantColor(image.Image, image);

            var imageWithColour = new ImageWithColour
            {
                Path = image.Image,
                Brightest = colours.ToHex(),
                Average = colours.ToHex(),
            };
            
            imagesWithColour.Add(imageWithColour);
        }

        return imagesWithColour;
    }

    /// <summary>
    /// Download an image async, there is probably a neater way to do this.
    /// It's demonstrating the concept, no the implementation.
    /// </summary>
    private static async Task<Stream> DownloadImageAsync(string url)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var stream = new MemoryStream();
        await response.Content.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }

    private static async Task<Rgba32> GetDominantColor(string imagePath, FocalPointRectangle focusArea, int resizeWidth = 16, int resizeHeight = 16)
    {
        // Download the image
        var imageStream = await DownloadImageAsync("https://localhost:44302" + imagePath);

        // Load the image with ImageSharp
        using var image = Image.Load<Rgba32>(imageStream);
        
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
}
