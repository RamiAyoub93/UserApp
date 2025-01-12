
using System.Threading.Tasks;

namespace MainApp.Dialogs
{
    public class Dialogs
    {
        public static void MenuHeading(string heading)
        {
            Console.Clear();
            Console.WriteLine($"///////////////// {heading.ToUpper()} /////////////////");
            Console.WriteLine();
        }


        public static string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine()!;
        }



    }
}
