using System.Drawing;
using DataMatrix_Lib.Classes;

namespace DataMatrix_Lib.Core
{
    public class DataMatrixCore
    {
        public Bitmap ConvertStringToDataMatrix(string value)
        {
            string base64 = Converter.StringToBase64(value);
            string binaryString = Converter.Base64ToBinary(base64);

            int length = binaryString.Length;
            int size = (int)Math.Ceiling(Math.Sqrt(length));

            Bitmap dataMatrix = new Bitmap(size, size);

            for (int i = 0; i < length; i++)
            {
                int x = i % size;
                int y = i / size;
                Color pixelColor = binaryString[i] == '1' ? Color.Black : Color.White;
                dataMatrix.SetPixel(x, y, pixelColor);
            }

            return dataMatrix;
        }

        public void ConvertStringToDataMatrix(string path, string value)
        {
            Bitmap dataMatrix = ConvertStringToDataMatrix(value);
            dataMatrix.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        public string ConvertDataMatrixToString(Bitmap dataMatrix)
        {
            string binaryString = String.Empty;

            for (int y = 0; y < dataMatrix.Height; y++)
            {
                for (int x = 0; x < dataMatrix.Width; x++)
                {
                    Color pixelColor = dataMatrix.GetPixel(x, y);

                    if (pixelColor == Color.FromArgb(255, 0, 0, 0))
                    {
                        binaryString += "1";
                    }
                    else if (pixelColor == Color.FromArgb(255, 255, 255, 255))
                    {
                        binaryString += "0";
                    }
                }
            }

            string base64 = Converter.BinaryToBase64(binaryString);
            return Converter.Base64ToAscii(base64);
        }

        public string ConvertDataMatrixToString(string path)
        {
            Bitmap dataMatrix = new Bitmap(path);
            return ConvertDataMatrixToString(dataMatrix);
        }

        public void ConvertFileToDataMatrix(string savePath, string filePath)
        {
            string base64 = Converter.FileToBase64(filePath);
            ConvertStringToDataMatrix(savePath, base64);
        }

        public void ConvertDataMatrixToFile(string savePath, string filePath)
        {
            string base64 = ConvertDataMatrixToString(filePath);
            Converter.Base64ToFile(savePath, base64);
        }
    }
}
