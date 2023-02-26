namespace DataMatrix_Lib.v02.Classes
{
    public class ErrorManager
    {
        public static void SendError(Exception ex)
        {
            Console.WriteLine("\n\n\n\n");
            Console.WriteLine(ex.Message);
            Console.ReadKey();
        }
    }
}
