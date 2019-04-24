using System;
using System.Diagnostics;

namespace Cheval
{
    public class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            Cheval.Run();
            stopwatch.Stop();

            // Write result.
            Console.WriteLine($"Total Time elapsed: {stopwatch.Elapsed}");
        }
    }
}
