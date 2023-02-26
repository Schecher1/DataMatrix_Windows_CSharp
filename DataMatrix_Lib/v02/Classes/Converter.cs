using System.Text;

namespace DataMatrix_Lib.v02.Classes
{
    public class Converter
    {
        public static string Base64ToBinary(string base64)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(base64);
            return string.Join("", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
        }

        public static string CharArrayToBase64(char[] value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        public static char[] TrimEnd(char[] array)
        {
            int i = array.Length - 1;
            while (i >= 0 && array[i] == '\0')
            {
                i--;
            }
            return array[..(i + 1)];
        }
    }
}
