using System.Text;

namespace DataMatrix_Lib.Classes
{
    public class Converter
    {
        public static string Base64ToAscii(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string FileToBase64(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(bytes);
        }

        public static string Base64ToBinary(string base64)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(base64);
            return string.Join("", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
        }

        public static string BinaryToBase64(string binary)
        {
            if (binary == null)
                throw new ArgumentNullException(nameof(binary));

            if (binary.Length % 8 != 0)
                throw new ArgumentException("The binary string length must be a multiple of 8", nameof(binary));

            byte[] bytes = new byte[binary.Length / 8];

            for (int i = 0; i < bytes.Length; i++)
            {
                Console.WriteLine((char)bytes[i]);
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string byteString = binary.Substring(i, 8);
                byte b = Convert.ToByte(byteString, 2);
                bytes[i / 8] = b;
            }

            return Encoding.ASCII.GetString(bytes);
        }

        internal static void Base64ToFile(string savePath, string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(savePath, bytes);
        }

        internal static string StringToBase64(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }
    }
}
