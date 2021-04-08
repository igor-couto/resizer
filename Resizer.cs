using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Image = System.Drawing.Image;

namespace resizer
{
    public class Resizer
    {
        private Arguments _arguments;

        public Resizer(Arguments arguments) => _arguments = arguments;

        public void Run()
        {
            var currentPath = Directory.GetCurrentDirectory();

            if(_arguments.ConvertAllFilesInFolder)
            {
                ProcessDirectory(currentPath, _arguments.IsRecursiveSearch);
                return;
            }

            if(IsImage(_arguments.FileName))
            {
                var image = Image.FromFile(Path.GetFullPath(currentPath + "\\" + _arguments.FileName));
                var newImage = ResizeImage(image, _arguments.Width, _arguments.Height);
                newImage.Save(currentPath + "\\novo.png");
            }
            else
                Console.WriteLine("The file is not an image");
        }

        void ProcessDirectory(string path, bool recursive)
        {
            var fileEntries = Directory.GetFiles(path);
            foreach(string fileName in fileEntries)
            {
                if(IsImage(fileName))
                {
                    var image = Image.FromFile(path + fileName);
                    ResizeImage(image, _arguments.Width, _arguments.Height);
                }
            }

            if(recursive)
            {
                var subdirectoryEntries = Directory.GetDirectories(path);
                foreach(string subdirectory in subdirectoryEntries)
                    ProcessDirectory(subdirectory, true);
            }
        }

        bool IsImage(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if(extension == ".png" || extension == ".jpeg" || extension == ".bmp" || extension == ".jpg")
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
            graphics.CompositingMode = _arguments.CompositingMode;
            graphics.CompositingQuality = _arguments.CompositingQuality;
            graphics.InterpolationMode = _arguments.InterpolationMode;
            graphics.SmoothingMode = _arguments.SmoothingMode;
            graphics.PixelOffsetMode = _arguments.PixelOffsetMode;

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            
            return destImage;
        }
    }
}