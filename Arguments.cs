using System;
using System.Drawing.Drawing2D;
using CommandLine;

namespace resizer
{
    public class Arguments
    {
        [Option(Required = true, HelpText = "")]
        public string FileName {get;}
        public bool ConvertAllFilesInFolder {get;}
        
        [Option('r', "recursive", Required = false, Default = false )]
        public bool IsRecursiveSearch {get;}
        
        [Option('w', "width", Required = true )]
        public int Width {get;}

        [Option('h', "height", Required = true )]
        public int Height {get;}

        public CompositingMode CompositingMode {get;}
        public CompositingQuality CompositingQuality {get;} = CompositingQuality.HighQuality;
        public InterpolationMode InterpolationMode {get;} = InterpolationMode.HighQualityBicubic;
        public SmoothingMode SmoothingMode {get;} = SmoothingMode.HighQuality;
        public PixelOffsetMode PixelOffsetMode {get;} = PixelOffsetMode.HighQuality;

        public Arguments(string[] args)
        {
            if(args[0] == ".")
                ConvertAllFilesInFolder = true;
            else
                FileName = args[0];

            if(int.TryParse(args[1], out var width))
            {
                if(width <= 0)
                {
                    Console.WriteLine("New size cannot be less than 0");
                    return;
                }
                Width = width;
            }

            if(int.TryParse(args[2], out var height))
            {
                if(height <= 0)
                {
                    Console.WriteLine("New size cannot be less than 0");
                    return;
                }
                Height = height;
            }

            for(var i = 3; i < args.Length; i++)
            {
                if(args[i] == "-r" || args[i] == "--recursive")
                    IsRecursiveSearch = true;

                if(args[i] == "--compositing" || args[i] == "-c")
                {
                    if(args[i+1] == "source-over")
                        CompositingMode = CompositingMode.SourceOver;
                    else
                    if(args[i+1] == "source-copy")
                        CompositingMode = CompositingMode.SourceCopy;
                    else
                        Console.WriteLine("Fail to identify the Compositing Mode");
                }

                if(args[i] == "--compositing-quality" || args[i] == "-cq")
                {
                    switch(args[i + 1])
                    {
                        case "default": 
                            CompositingQuality = CompositingQuality.Default;
                            break;
                        case "high-speed": 
                            CompositingQuality = CompositingQuality.Default;
                        break;
                        case "high-quality": 
                            CompositingQuality = CompositingQuality.Default;
                        break;
                        case "gamma-corrected": 
                            CompositingQuality = CompositingQuality.Default;
                        break;
                        case "assume-linear": 
                            CompositingQuality = CompositingQuality.Default;
                        break;
                    }
                }

                if(args[i] == "--interpolation" || args[i] == "-i")
                {
                    switch(args[i + 1])
                    {
                        case "default": 
                            InterpolationMode = InterpolationMode.Default;
                            break;
                        case "low": 
                            InterpolationMode = InterpolationMode.Low;
                        break;
                        case "high": 
                            InterpolationMode = InterpolationMode.High;
                        break;
                        case "bilinear": 
                            InterpolationMode = InterpolationMode.Bilinear;
                        break;
                        case "bicubic": 
                            InterpolationMode = InterpolationMode.Bicubic;
                        break;
                        case "nearest-neighbor": 
                            InterpolationMode = InterpolationMode.NearestNeighbor;
                        break;
                        case "high-quality-bilinear": 
                            InterpolationMode = InterpolationMode.HighQualityBilinear;
                        break;
                        case "high-quality-bicubic": 
                            InterpolationMode = InterpolationMode.HighQualityBicubic;
                        break;
                    }
                }

                if(args[i] == "--smoothing" || args[i] == "-s")
                {
                    switch(args[i + 1])
                    {
                        case "default": 
                            SmoothingMode = SmoothingMode.Default;
                            break;
                        case "high-speed": 
                            SmoothingMode = SmoothingMode.HighSpeed;
                        break;
                        case "high-quality": 
                            SmoothingMode = SmoothingMode.HighQuality;
                        break;
                        case "none": 
                            SmoothingMode = SmoothingMode.None;
                        break;
                        case "anti-alias": 
                            SmoothingMode = SmoothingMode.AntiAlias;
                        break;
                    }
                }
                    
                if(args[i] == "--pixel-offset" || args[i] == "-po")
                {
                    switch(args[i + 1])
                    {
                        case "default": 
                            PixelOffsetMode = PixelOffsetMode.Default;
                            break;
                        case "high-speed": 
                            PixelOffsetMode = PixelOffsetMode.HighSpeed;
                        break;
                        case "high-quality": 
                            PixelOffsetMode = PixelOffsetMode.HighQuality;
                        break;
                        case "none": 
                            PixelOffsetMode = PixelOffsetMode.None;
                        break;
                        case "half": 
                            PixelOffsetMode = PixelOffsetMode.Half;
                        break;
                    }
                }
            }
        }
    }
}