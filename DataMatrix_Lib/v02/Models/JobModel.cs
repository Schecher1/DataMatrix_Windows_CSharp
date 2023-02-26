namespace DataMatrix_Lib.v02.Models
{
    public class JobModel
    {
        //RF         => ReadFile
        //B2B64C => BytesToBase64Convert
        //B642BC => Base64ToBinaryConvert
        //B2BC     => BinaryToBitmapConvert

        public readonly int MAX_B2B64C_QUEUE_SIZE = 320;
        public readonly int MAX_B2BC_QUEUE_SIZE = 320;
        public readonly int MAX_B642BC_QUEUE_SIZE = 320;
        public readonly int DATAMATRIX_HEIGHT = 10_000;
        public readonly int DATAMATRIX_WIDTH = 10_000;

        public readonly string FilePath;
        public readonly string DataMatrixPath;

        public volatile bool Job_RF_Finish = false;
        public volatile bool Job_B2B64C_Finish = false;
        public volatile bool Job_B642BC_Finish = false;
        public volatile bool Job_B2BC_Finish = false;

        private Queue<SubJobModel> b2b64cQueue = new Queue<SubJobModel>();
        private Queue<SubJobModel> b642bcQueue = new Queue<SubJobModel>();
        private Queue<SubJobModel> b2bcQueue = new Queue<SubJobModel>();

        public volatile StreamReader _sr;

        public readonly int CHUNK_SIZE = 32 * 1024; // 32 kb
        private int subJobID = 0;
        private object subJobIDLock = new object();

        public bool IsJobFinished { get { return Job_RF_Finish && Job_B2B64C_Finish && Job_B642BC_Finish && Job_B2BC_Finish;}}
        public int GetMaxSubModelID { get { return Job_RF_Finish ? subJobID : -1; } }

        public JobModel(string filePath, string dataMatrixPath)
        {
            this.FilePath = filePath;
            this.DataMatrixPath = dataMatrixPath;
            b2b64cQueue = new Queue<SubJobModel>();
            b642bcQueue = new Queue<SubJobModel>();
            b2bcQueue = new Queue<SubJobModel>();
            _sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, CHUNK_SIZE, useAsync: true));
        }

        public SubJobModel B2B64CQueue
        {
            get
            {
                lock (b2b64cQueue)
                {
                    if (b2b64cQueue.Count > 0)
                        return b2b64cQueue.Dequeue();
                    else
                        return null;
                }
            }
            set
            {
                lock (b2b64cQueue) { b2b64cQueue.Enqueue(value); }
            }
        }
        
        public SubJobModel B642BCQueue
        {
            get
            {
                lock (b642bcQueue)
                {
                    if (b642bcQueue.Count > 0)
                        return b642bcQueue.Dequeue();
                    else
                        return null;
                }
            }
            set
            {
                lock (b642bcQueue) { b642bcQueue.Enqueue(value); }
            }
        }
        
        public SubJobModel B2BCQueue
        {
            get
            {
                lock (b2bcQueue)
                {
                    if (b2bcQueue.Count > 0)
                        return b2bcQueue.Dequeue();
                    else
                        return null;
                }
            }
            set
            {
                lock (b2bcQueue) { b2bcQueue.Enqueue(value); }
            }
        }

        public int B2B64CQueueCount { get { return b2b64cQueue.Count; } }
        
        public int B642BCQueueCount { get { return b642bcQueue.Count; } }

        public int B2BCQueueCount { get { return b2bcQueue.Count; } }

        public int GetSubJobID()
        {
            lock (subJobIDLock)
            {
                return Interlocked.Increment(ref subJobID);
            }
        }
    }
}
