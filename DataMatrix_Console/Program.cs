using DataMatrix_Lib.v02.Classes;
using DataMatrix_Lib.v02.Core;
using DataMatrixCore = DataMatrix_Lib.v02.Core.DataMatrixCore;

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
                Console.WriteLine("             DataMatrix Builder   |    Version 1.1.1.0   |  GitHub: Schecher1/DataMatrix_Windows_CSharp");
                Console.WriteLine(MENU_LAYOUT);
                Console.WriteLine();
                Console.WriteLine("0 => DEBUG");
                Console.WriteLine("1 => Convert String to DataMatrix-Image");
                Console.WriteLine("2 => Convert DataMatrix-Image to String");
                Console.WriteLine("3 => Convert File to DataMatrix-Image");
                Console.WriteLine("4 => Convert DataMatrix-Image to File");
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine(MENU_LAYOUT);
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        Debug();
                        break;
                        
                    case "1":
                        ConvertStringToDataMatrixImage();
                        break;
                        
                    case "2":
                        ConvertDataMatrixImageToString();
                        break;

                    case "3":
                        ConvertFileToDataMatrixImage();
                        break;

                    case "4":
                        ConvertDataMatrixImageToFile();
                        break;

                    case "q":
                        isRunning = false;
                        break;
                        
                    default:
                        break;
                }

                //just be on the safe side
                GC.Collect();
            }
        }

        private void Debug()
        {
            string filePath, DataMatrixPath;

            filePath = GetUserInput("Enter the path where your file is located, which should be processed to a DataMatrix. (please include the file extension)", "File-Path: ");
            DataMatrixPath = GetUserInput("Enter the path where your datamatrix should be stored ", "DataMatrix-Path: ");

            DataMatrixCore _dmc = new DataMatrixCore();
            _dmc.ConvertFileToDataMatrix(filePath, DataMatrixPath);
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

        private void ConvertStringToDataMatrixImage()
        {
            string path, value;

            path = GetUserInput("Enter a path where the DataMatrix should be saved (please include the file name + .png)", "Path: ");
            value = GetUserInput("Enter your text, which should be converted to DataMatrix", "Text: ");

            throw new NotImplementedException();

            //DataMatrixCore _dmc = new DataMatrixCore();
            //try{ _dmc.ConvertStringToDataMatrix(path, value); } catch (Exception ex){ ErrorManager.SendError(ex); }
        }

        private void ConvertDataMatrixImageToString()
        {
            string path;
            string output = string.Empty;

            path = GetUserInput("Enter the path to the DataMatrix (please include the file name + .png)", "Path: ");

            DataMatrix_Lib.v01.Core.DataMatrixCore _dmc = new DataMatrix_Lib.v01.Core.DataMatrixCore();
            try { output = _dmc.ConvertDataMatrixToString(path); } catch (Exception ex) { ErrorManager.SendError(ex); }

            Console.WriteLine();
            Console.WriteLine("The value of the Datamatrix is: " + output);
            Console.ReadKey();
        }

        private void ConvertFileToDataMatrixImage()
        {
            string filePath, savePath;

            filePath = GetUserInput("Enter a path where the file that converted into a DataMatrix (please include the file name + extension)", "Path: ");
            savePath = GetUserInput("Enter a path where the DataMatrix should be saved (please include the file name  + .png)", "Path: ");

            DataMatrixCore _dmc = new DataMatrixCore();
            DateTime start = DateTime.Now;
            //try { _dmc.ConvertFileToDataMatrix(savePath, filePath); } catch (Exception ex) { ErrorManager.SendError(ex); }
            _dmc.ConvertFileToDataMatrix(savePath, filePath);
            DateTime end = DateTime.Now;
            Console.WriteLine("Time needed: " + (end - start).TotalMinutes + " minutes");
            Console.WriteLine("Time needed: " + (end - start).TotalSeconds + " seconds");
            Console.ReadKey();
        }

        private void ConvertDataMatrixImageToFile()
        {
            string filePath, savePath;

            filePath = GetUserInput("Enter a path where the DataMatrix is located (please include the file name + .png)", "Path: ");
            savePath = GetUserInput("Enter a path where the file should be saved (please include the file name + extension)", "Path: ");

            DataMatrixCore _dmc = new DataMatrixCore();
            try { _dmc.ConvertDataMatrixToFile(savePath, filePath); } catch (Exception ex) { ErrorManager.SendError(ex); }
        }
    }
}