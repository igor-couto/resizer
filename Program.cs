using System;

namespace resizer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = new Arguments(args);
                new Resizer(arguments).Run();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}