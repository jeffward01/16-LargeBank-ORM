using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeBank_ORM
{
    class Program
    {
        //Properties
        public static string Line = "--------------------------- \n \n \n";

        static void Main(string[] args)
        {
            GreetingsMesage();
            ListAllCustomersANDAccountInformation();
         

        }
        public static void GreetingsMesage()
        {
            Console.WriteLine(Line);
            Console.WriteLine("You have entered the Command Station for Bank Administration");
            Console.WriteLine("Query Data and More!!");
            Console.WriteLine(Line);
            Console.WriteLine("Please press Enter to Continue...");
            Console.ReadLine();
            Console.Clear();

        }
        public static void ListAllCustomersANDAccountInformation()
        {
            using (var db = new LargeBankEntities())
            {
                foreach (var customer in db.Customers)
                {
                    Console.WriteLine("Customer First Name and Last Name: " + customer.FirstName +" "+customer.LastName+".");

                    foreach (var account in customer.Accounts)
                    {
                        Console.WriteLine("The balance for account " + account.AccountNumber + " is: " + account.Balance.ToString("C")+ Environment.NewLine);             
                    }
                }

                Console.WriteLine("Please press Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
