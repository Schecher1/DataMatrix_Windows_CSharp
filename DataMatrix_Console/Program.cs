using DataMatrix_Lib.Core;
using System.IO;

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
                Console.WriteLine();
                Console.WriteLine("             DataMatrix Builder   |    Version 1.0.0.0   |  GitHub: Schecher1/DataMatrix_Windows_CSharp");
                Console.WriteLine(MENU_LAYOUT);
                Console.WriteLine();
                Console.WriteLine("1 => Convert String to DataMatrix");
                Console.WriteLine("2 => Convert DataMatrix to String");
                Console.WriteLine("3 => Convert File to DataMatrix");
                Console.WriteLine("4 => Convert DataMatrix to File");
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

                    case "3":
                        ConvertFileToDataMatrix();
                        break;

                    case "4":
                        ConvertDataMatrixToFile();
                        break;

                    case "q":
                        isRunning = false;
                        break;
                        
                    default:
                        break;
                }
            }
        }

        private string GetUserInput(string message, string keyWord)
        {
            string? input = string.Empty;
            
            while (String.IsNullOrWhiteSpace(input))
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(message);
                Console.Write(keyWord);
                input = Console.ReadLine();
            }

            return input;
        }

        private void ConvertStringToDataMatrix()
        {
            string path, value;

            path = GetUserInput("Enter a path where the DataMatrix should be saved (please include the file name + .png)", "Path: ");
            value = GetUserInput("Enter your text, which should be converted to DataMatrix", "Text: ");
            
            DataMatrixCore _dmc = new DataMatrixCore();
            _dmc.ConvertStringToDataMatrix(path, value);
        }

        private void ConvertDataMatrixToString()
        {
            string path;

            path = GetUserInput("Enter the path to the DataMatrix (please include the file name + .png)", "Path: ");

            DataMatrixCore _dmc = new DataMatrixCore();
            string output = _dmc.ConvertDataMatrixToString(path);

            Console.WriteLine();
            Console.WriteLine("The value of the Datamatrix is: " + output);
            Console.ReadKey();
        }

        private void ConvertFileToDataMatrix()
        {
            string filePath, savePath;

            savePath = GetUserInput("Enter a path where the DataMatrix should be saved (please include the file name  + .png)", "Path: ");
            filePath = GetUserInput("Enter a path where the file that converted into a DataMatrix (please include the file name + extension)", "Path: ");

            DataMatrixCore _dmc = new DataMatrixCore();
            _dmc.ConvertFileToDataMatrix(savePath, filePath);
        }

        private void ConvertDataMatrixToFile()
        {
            string filePath, savePath;

            filePath = GetUserInput("Enter a path where the DataMatrix is located (please include the file name + .png)", "Path: ");
            savePath = GetUserInput("Enter a path where the file should be saved (please include the file name + extension)", "Path: ");

            DataMatrixCore _dmc = new DataMatrixCore();
            _dmc.ConvertDataMatrixToFile(savePath, filePath);
        }
    }
}