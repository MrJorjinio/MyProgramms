using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class UserHome
    { 
        public string userHomeFirm { get; set; }
        public string userHomeAdress { get; set; }

        public double userHomeSize { get; set; }
        public string userHomeType { get; set; }
        public double userHomePrice { get; set; }
        public int userHomeRooms { get; set; }
        public string userName { get; set; }

        public void userHomeFillUp()
        {
            Console.WriteLine("Enter your home firm: ");
            userHomeFirm = Console.ReadLine();
            Console.WriteLine("Enter your home adress: ");
            userHomeAdress = Console.ReadLine();
            Console.WriteLine("Enter your home size(m²): ");
            userHomeSize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your home type(Economy, Normal, Luxury): ");
            userHomeType = Console.ReadLine();
            Console.WriteLine("Enter your home price($): ");
            userHomePrice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your home rooms: ");
            userHomeRooms = Convert.ToInt32(Console.ReadLine());
        }

        public void userHomeShow()
        {
            Console.WriteLine("Your home firm: " + userHomeFirm);
            Console.WriteLine("Your home adress: " + userHomeAdress);
            Console.WriteLine("Your home size: " + userHomeSize + "m²");
            Console.WriteLine("Your home type: " + userHomeType);
            Console.WriteLine("Your home price: $" + userHomePrice);
            Console.WriteLine("Your home rooms: " + userHomeRooms);
        }

    }
}
