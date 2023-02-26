using DataMatrix_Lib.v02.Classes;
using DataMatrix_Lib.v02.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace DataMatrix_Lib.v02.Core
{
    public class WorkerCore
    {
        private const int WORKER_RF_COUNT = 10;
        private const int WORKER_B2B64C_COUNT = 10;
        private const int WORKER_B642BC_COUNT = 10;
        private const int WORKER_B2BC_COUNT = 10;
        
        List<Thread> workers;
        JobModel _job;

        private const int SLEEP = 250; //250ms

        public WorkerCore(JobModel job)
        {
            _job = job;
            InitThreads();
            StartThread();
        }

        private void InitThreads()
        {
            workers = new List<Thread>();

            for (int i = 1; i <= WORKER_RF_COUNT; i++)
                workers.Add(new Thread(Work_ReadFile));

            for (int i = 1; i <= WORKER_B2B64C_COUNT; i++)
                workers.Add(new Thread(Work_BytesToBase64Convert));

            for (int i = 1; i <= WORKER_B642BC_COUNT; i++)
                workers.Add(new Thread(Work_Base64ToBinaryConvert));

            for (int i = 1; i <= WORKER_B2BC_COUNT; i++)
                workers.Add(new Thread(Work_BinaryToBitmapConvert));
        }

        private void StartThread()
        {
            foreach (Thread worker in workers)
            {
                worker.IsBackground = true;
                worker.Start();
            }
        }

        public void Work_ReadFile()
        {
            //read the file in char chunks (max 32kb) until the end of the file
            //put the byte array into the queue (list of char[])
            //if the list is full (64), wait until he has space

            bool workIsFinish = false;

#if DEBUG
            DebugLog("RF", "Worker started");
#endif

            while (!workIsFinish)
            {
                char[] buffer = null;

                //if the queue is full
                if (_job.B2B64CQueueCount >= _job.MAX_B2B64C_QUEUE_SIZE)
                {
                    //sleep
#if DEBUG
                    DebugLog("RF", "Queue full, wait");
#endif
                    Waiter();
                    continue;
                }

#if DEBUG
                DebugLog("RF", "Get buffer");
#endif


                //get chunk
                lock (_job._sr)
                {
                    buffer = new char[_job.CHUNK_SIZE];
                    if (_job._sr.Read(buffer) == 0)
                        workIsFinish = true;
                }

                //removed the zero bytes
                buffer = Converter.TrimEnd(buffer);

                //restart the work, if the buffer is empty (can happen at the end, if tow or more threads are working on a empty file)
                if (buffer.Length == 0)
                    continue;

                //add the chunk to Queue
                _job.B2B64CQueue = new SubJobModel { SubJobID = _job.GetSubJobID(), CharArray = buffer };
#if DEBUG
                    DebugLog("RF", "Add to queue");
#endif
            }
            _job.Job_RF_Finish = true;
#if DEBUG
            DebugLog("RF", "Worker Finished");
#endif
        }

        public void Work_BytesToBase64Convert()
        {
            //get the byte array from the queue (list of char[])
            //convert the byte array to base64 string
            //put the base64 string into the queue (list of base64Strings)
            //if the list is full (64), wait until he has space.

            bool workIsFinish = false;
            
#if DEBUG
            DebugLog("B2B64C", "Worker started");
#endif

            while (!workIsFinish)
            {
                //if the queue is full
                if (_job.B642BCQueueCount >= _job.MAX_B642BC_QUEUE_SIZE)
                {
                    //sleep
#if DEBUG
                    DebugLog("B2B64C", "Queue full, wait");
#endif
                    Waiter();
                    continue;
                }

                //if the job is finish
                if (_job.B2B64CQueueCount == 0 && _job.Job_RF_Finish)
                {
                    workIsFinish = true;
                    continue;
                }
                
                SubJobModel newJob = _job.B2B64CQueue;

                //get job, if possible
                if (newJob is null)
                {
#if DEBUG
                    DebugLog("B2B64C", "Nothing there to do, idle");
#endif
                    Waiter();
                    continue;
                }
                
#if DEBUG
                DebugLog("B2B64C", "Get job");
#endif

                newJob.Base64String = Converter.CharArrayToBase64(newJob.CharArray);

                //add the chunk to Queue
                _job.B642BCQueue = newJob;
#if DEBUG
                DebugLog("B2B64C", "add to queue");
#endif
            }
            _job.Job_B2B64C_Finish = true;
#if DEBUG
            DebugLog("B2B64C", "Worker finished");
#endif
        }

        public void Work_Base64ToBinaryConvert()
        {
            //get the base64 string from the queue (list of base64Strings)
            //convert the base64 string to binary string
            //put the binary string into the queue (list of binaryStrings)
            //if the list is full (64), wait until he has space.

            bool workIsFinish = false;
#if DEBUG
            DebugLog("B642BC", "Worker started");
#endif

            while (!workIsFinish)
            {
                //if the queue is full
                if (_job.B2BCQueueCount >= _job.MAX_B2BC_QUEUE_SIZE)
                {
                    //sleep
#if DEBUG
                    DebugLog("B642BC", "Queue full, wait");
#endif
                    Waiter();
                    continue;
                }

                //if the job is finish
                if (_job.B642BCQueueCount == 0 && _job.Job_B2B64C_Finish)
                {
                    workIsFinish = true;
                    continue;
                }


                SubJobModel newJob = _job.B642BCQueue;

                //get job, if possible
                if (newJob is null)
                {
#if DEBUG
                    DebugLog("B642BC", "Nothing there to do, idle");
#endif
                    Waiter();
                    continue;
                }

#if DEBUG
                DebugLog("B642BC", "Get job");
#endif

                newJob.BinaryString = Converter.Base64ToBinary(newJob.Base64String);

                //add the chunk to Queue
                _job.B2BCQueue = newJob;
#if DEBUG
                DebugLog("B642BC", "add to queue");
#endif
            }
            _job.Job_B642BC_Finish = true;
#if DEBUG
            DebugLog("B642BC", "Worker finished");
#endif
        }

        public async void Work_BinaryToBitmapConvert()
        {
            //get the binary string from the queue (list of binaryStrings)
            //write the pixel to the places
            //wait if the bitmap is finish and saves then

            bool workIsFinish = false;
#if DEBUG
            DebugLog("B2BC", "Worker started");
#endif

            while (!workIsFinish)
            {
                //WaitForTheOtherWorkers();
                //SortTheB2BCQueue();
                
                if (_job.B2BCQueueCount == 0 && _job.Job_B642BC_Finish)
                {
                    workIsFinish = true;
                    _job.Job_B2BC_Finish = true;
                    continue;
                }

                SubJobModel newJob = _job.B2BCQueue;

                if (newJob is null)
                {
#if DEBUG
                    DebugLog("B2BC", "Nothing there to do, idle");
#endif
                    Waiter();
                    continue;
                }

#if DEBUG
                DebugLog("B2BC", "Get job");
#endif
                    
                //painting the datamatrix
                int length = newJob.BinaryString.Length;
                int size = (int)Math.Ceiling(Math.Sqrt(length));

                Bitmap dataMatrix = new Bitmap(size, size);

                for (int i = 0; i < length; i++)
                {
                    int x = (int)(i % size);
                    int y = (int)(i / size);
                    Color pixelColor = newJob.BinaryString[(int)i] == '1' ? Color.Black : Color.White;
                    dataMatrix.SetPixel(x, y, pixelColor);
                }

                await SaveBitmap(newJob, dataMatrix);
            }
            _job.Job_B2BC_Finish = true;
#if DEBUG
            DebugLog("B2BC", "Worker finished");
#endif
        }
        
        private async Task SaveBitmap(SubJobModel saveJob, Bitmap dataMatrix)
        {
            await Task.Run(() => 
            {
                //Configure JPEG compression
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 50L); // ANYTHING UNDER 50L DESTROYS THE DATAMATRIX (Pixels are shifted, because of the compression)

                dataMatrix.Save($"{_job.DataMatrixPath}\\{saveJob.SubJobID}.jpg", jpgEncoder, encoderParameters);
            });
        }

        private ImageCodecInfo GetEncoder(ImageFormat jpeg)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == jpeg.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void Waiter()
            => Thread.Sleep(SLEEP);

        private void DebugLog(string prefix, string message)
            => Console.WriteLine($"[{prefix}]: {message}");
    }
}
