using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal static class Connection
    {
        public static string host {  get; set; }
        public static string port { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }
        public static string connectionString { get; set; }

        public static bool ConnectToPostgre()
        {
            Console.WriteLine("Enter your host(Default: localhost):");
            string host = Console.ReadLine();
            if (string.IsNullOrEmpty(host))
            {
                host = "localhost";
            }
            else
            {
                host = host.Trim();
            }
            Connection.host = host;
            Console.Clear();
            Console.WriteLine("Enter your port(Default: 5432):");
            string port = Console.ReadLine();
            if (string.IsNullOrEmpty(port))
            {
                port = "5432";
            }
            else
            {
                port = port.Trim();
            }
            Connection.port = port;
            Console.Clear();

            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            Connection.username = username;
            Console.Clear();
            Console.Write("Enter your password: ");
            string password = "";
            Console.WriteLine();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    password = Connection.password;
                    Console.Write("\b\b");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.Clear();
            Connection.connectionString = $"Host={Connection.host};Port={Connection.port};Username={Connection.username};Password={password};";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection failed!");
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
