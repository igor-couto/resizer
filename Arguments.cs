namespace resizer
{
    class Arguments
    {
        //  Graphics
        //  CompositingMode = CompositingMode.SourceCopy;
        //  CompositingQuality = CompositingQuality.Default;
        //  InterpolationMode = InterpolationMode.NearestNeighbor;
        //  SmoothingMode = SmoothingMode.None;
        //  PixelOffsetMode = PixelOffsetMode.None;

        public string FileName {get;}
        public bool ConvertAllFilesInFolder {get;}
        public bool IsRecursiveSearch {get;}
        public int Width {get;}
        public int Height {get;}


        public Arguments(string[] args)
        {
            //TODO: Parsing
        }
    }
}