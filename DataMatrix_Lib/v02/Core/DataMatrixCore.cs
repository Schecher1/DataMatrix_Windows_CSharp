using System.Drawing;
using DataMatrix_Lib.v02.Classes;
using DataMatrix_Lib.v02.Models;

namespace DataMatrix_Lib.v02.Core
{
    public class DataMatrixCore
    {
        //workers (threads)
        //file reader worker
        //bytes converter to base64 worker
        //base64 converter to binary worker
        //binary converter to bitmap worker

        //Step for step list
        //ConvertFileToDataMatrix (optimize for lage files)
        //read the file in bytes chunk chunks
        //convert the bytes chunk to base64
        //convert the base64 chunk to binary
        //write the binary chunk to the bitmap

        public void ConvertFileToDataMatrix(string filePath, string dataMatrixPath)
        {
            JobModel _jm = new JobModel(filePath, dataMatrixPath);

#if DEBUG
            DateTime start = DateTime.Now;
#endif

            WorkerCore _wc = new WorkerCore(_jm);

            while (!_jm.IsJobFinished)
                Thread.Sleep(50);

#if DEBUG
            DateTime end = DateTime.Now;
            TimeSpan ts = end - start;
            
            Console.WriteLine("Time: " + ts.TotalMinutes + "m");
            Console.WriteLine("Time: " + ts.TotalSeconds + "s");
            Console.WriteLine("Time: " + ts.TotalMilliseconds + "ms");
            Console.ReadKey();
#endif
        }

        public void ConvertDataMatrixToFile(string path, string value)
        {

        }
    }
}
