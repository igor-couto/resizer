using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Image = System.Drawing.Image;
using resizer;

var arguments = new Arguments(args);

var path = Directory.GetCurrentDirectory();

if (arguments.ConvertAllFilesInsideFolder)
{
    ProcessDirectory(path, arguments.IsRecursiveSearch);
    return;
}

if (IsImage(arguments.FileName))
{
    var image = Image.FromFile(Path.GetFullPath(path + "\\" + arguments.FileName));
    var newImage = ResizeImage(image, arguments.Width, arguments.Height);
    image.Dispose();
    newImage.Save(path + "\\" + arguments.FileName);
    newImage.Dispose();
}
else
    Console.WriteLine("The file is not an image");


void ProcessDirectory(string path, bool recursive)
{
    var fileEntries = Directory.GetFiles(path);
    foreach (string fileName in fileEntries)
    {
        if (IsImage(fileName))
        {
            var image = Image.FromFile(fileName);
            var newImage = ResizeImage(image, arguments.Width, arguments.Height);
            image.Dispose();
            newImage.Save(fileName);
            newImage.Dispose();
        }
    }

    if (recursive)
    {
        var subdirectoryEntries = Directory.GetDirectories(path);
        foreach (string subdirectory in subdirectoryEntries)
            ProcessDirectory(subdirectory, true);
    }
}

bool IsImage(string fileName)
{
    var extension = Path.GetExtension(fileName);
    if (extension == ".png" || extension == ".jpeg" || extension == ".bmp" || extension == ".jpg")
        return true;
    else
        return false;
}

Image ResizeImage(Image image, int width, int height)
{
    var destRect = new Rectangle(0, 0, width, height);
    var destImage = new Bitmap(width, height);

    destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

    using var graphics = Graphics.FromImage(destImage);
    graphics.CompositingMode = arguments.CompositingMode;
    graphics.CompositingQuality = arguments.CompositingQuality;
    graphics.InterpolationMode = arguments.InterpolationMode;
    graphics.SmoothingMode = arguments.SmoothingMode;
    graphics.PixelOffsetMode = arguments.PixelOffsetMode;

    using var wrapMode = new ImageAttributes();
    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);

    return destImage;
}