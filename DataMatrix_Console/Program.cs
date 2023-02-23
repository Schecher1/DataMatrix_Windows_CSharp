using DataMatrix_Lib.Core;

namespace DataMatrix_Console
{
    internal class Program
    {
        private const string MENU_LAYOUT = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
        static void Main(string[] args)
            => new Program().Start();

        public Program() { }


        public void Start()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("             DataMatrix Builder   |    Version 1.0.0.0   |  GitHub: Schecher1/DataMatrix_Windows_CSharp");
                Console.WriteLine(MENU_LAYOUT);
                Console.WriteLine();
                Console.WriteLine("1 => Convert String to DataMatrix");
                Console.WriteLine("2 => Convert DataMatrix to String");
                Console.WriteLine();
                Console.WriteLine("q => Exit");
                Console.WriteLine();
                Console.WriteLine(MENU_LAYOUT);
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ConvertStringToDataMatrix();
                        break;
                        
                    case "2":
                        ConvertDataMatrixToString();
                        break;
                        
                    case "q":
                        isRunning = false;
                        break;
                        
                    default:
                        break;
                }
            }
        }

        private void ConvertStringToDataMatrix()
        {
            string path, value;

            Console.WriteLine();
            Console.WriteLine("Please enter the path to save the DataMatrix");
            Console.Write("Path: ");
            path = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Please enter the value to convert to DataMatrix");
            Console.Write("Value: ");
            value = Console.ReadLine();
            
            DataMatrixCore _dmc = new DataMatrixCore();
            _dmc.ConvertStringToDataMatrix(path, value);
        }

        private void ConvertDataMatrixToString()
        {
            string path;

            Console.WriteLine();
            Console.WriteLine("Please specify the path where the DataMatrix is located");
            Console.Write("Path: ");
            path = Console.ReadLine();

            DataMatrixCore _dmc = new DataMatrixCore();
            string output = _dmc.ConvertDataMatrixToString(path);

            Console.WriteLine("The value of the Datamatrix is: " + output);

            Console.ReadKey();
        }
    }
}