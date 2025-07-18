namespace ConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        Start:
            if (Connection.ConnectToPostgre())
            {
                UserInterface.MainMenu();
            }
            else
            {
                goto Start;
            }
        }
    }
}
