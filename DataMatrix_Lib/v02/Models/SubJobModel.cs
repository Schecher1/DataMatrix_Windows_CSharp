namespace DataMatrix_Lib.v02.Models
{
    public class SubJobModel
    {
        public int SubJobID { get; set; }
        public char[] CharArray { get; set; } = Array.Empty<char>();
        public string Base64String { get; set; } = String.Empty;
        public string BinaryString { get; set; } = String.Empty;
    }
}
