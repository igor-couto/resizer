using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Image = System.Drawing.Image;

namespace resizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentPath = Directory.GetCurrentDirectory();

            if(args[0] == ".")
            {
                ProcessDirectory(currentPath, false);
                return;
            }

            if(IsImage(args[0]))
            {
                var image = Image.FromFile(Path.GetFullPath(currentPath + "\\" + args[0]));
                var newImage = ResizeImage(image, 100, 100);
                newImage.Save(currentPath + "\\novo.png");
            }
            else
                Console.WriteLine("The file is not an image");
        }

        static void ProcessDirectory(string path, bool recursive)
        {
            var fileEntries = Directory.GetFiles(path);
            foreach(string fileName in fileEntries)
            {
                if(IsImage(fileName))
                {
                    var image = Image.FromFile(path + fileName);
                    ResizeImage(image, 100, 100);
                }
            }

            if(recursive)
            {
                var subdirectoryEntries = Directory.GetDirectories(path);
                foreach(string subdirectory in subdirectoryEntries)
                    ProcessDirectory(subdirectory, true);
            }
        }

        static bool IsImage(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if(extension == ".png" || extension == ".jpeg" || extension == ".bmp" || extension == ".jpg")
                return true;
            else 
                return false;
        }

        static Image ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using var graphics = Graphics.FromImage(destImage);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.Default;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.PixelOffsetMode = PixelOffsetMode.None;

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            
            return destImage;
        }
    }
}