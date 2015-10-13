using LargeBank_ORM.Classes;
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
        public static string UserFirstName;

        static void Main(string[] args)
        {
            GetUserInfo();
            GreetingsMesage(UserFirstName);
            ActionController();
                         
        }

        //Methods
        public static void GetUserInfo()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine(Line);
                    Console.WriteLine("Welcome User!!! \n \n \n Please enter your first Name for Authentication Purposes...");
                    string input = Console.ReadLine();


                    if (input == null || input == "")
                    {
                        Console.WriteLine("Please enter a name!! Cannot be blank...");
                        continue;
                    }
                    else
                    {
                        UserFirstName = input;
                        break;
                    }
                }
                catch
                {
                }           
            }       
            Console.Clear();
        }
        public static void GreetingsMesage(string user)
        {
            Console.WriteLine(Line);
            Console.WriteLine("Welcome Lord {0}!", user);
            Console.WriteLine("You have entered the Command Station for Bank Administration");
            Console.WriteLine("Query Data and More!!");
            Console.WriteLine(Line);
            Console.WriteLine("Please press Enter to Continue...");
            Console.ReadLine();
            Console.Clear();

        }

        public static void ActionController()
        {
            Console.WriteLine(Line);
            Console.WriteLine("----- Welcome Lord {0} to the ------- MAIN SCREEN ----- \n \n", UserFirstName);
            Console.WriteLine("What would you like to do???  Please enter the number of the option you wish to choose.... \n ");
            Console.WriteLine("1. View all Customers Accounts and Transactions");
            Console.WriteLine("2. Add Information (Customers, Accounts, Transactions)");
            Console.WriteLine("3. Exit Program");
            string input = Console.ReadLine().ToLower();

        switch(input)
            {
                case "1":
                    Console.Clear();
                    ListAllCustomersANDAccountInformation();
                    break;
                case "2":
                    Console.Clear();
                    AddInformationController();
                    break;
                case "3":
                    Console.Clear();
                    ExitPrompt();
                    break;
                default:
                    Console.Clear();
                    ActionController();
                    break;
            }
        }

        public static void AddInformationController()
        {
            Console.WriteLine(Line);
            Console.WriteLine("Welcome Lord {0} to the Add Customer Information of ANY Kind Station.");
            Console.WriteLine("What would you like to add? \n ");
            Console.WriteLine("Please type the option of the action you would like to execute...");
            Console.WriteLine("1. Add new Customer.");
            Console.WriteLine("2. Add Accounts to a Customer.");
            Console.WriteLine("3.  Add Transactions to an Account.");
            Console.WriteLine("4. Remove Transactions from a Customer.");
            Console.WriteLine("5. Remove Accounts (and all Transactions) from a Customer.");
            Console.WriteLine("6. Go back to Main Menu. ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Clear();
                    GetSAVENewCustomerInfo();
                    break;
                case "2":
                    Console.Clear();
                    AddAccountsToCustomerPrompt();
                    break;
                case "3":
                    Console.Clear();
                    //Insert Method Here
                    break;
                case "4":
                    Console.Clear();
                    //Insert Method Here
                    break;
                case "5":
                    Console.Clear();
                    //Insert Method Here
                    break;
                case "6":
                    Console.Clear();
                    ActionController();
                    break;

                default:
                    Console.Clear();
                    AddInformationController();
                    break;
            }
        }

        public static void ListAllCustomersANDAccountInformation()
        {
            using (var db = new LargeBankEntities())
            {
                foreach (var customer in db.Customers)
                {
                    Console.WriteLine(" \n Customer First Name and Last Name: " + customer.FirstName +" "+customer.LastName+".");

                    foreach (var account in customer.Accounts)
                    {
                        Console.WriteLine("The balance for account " + account.AccountNumber + " is: " + account.Balance.ToString("C"));             
                    }
                }

                //Go back to Main Menu
                Console.WriteLine(Line);
                Console.WriteLine("Press Enter to return to Main Menu...");
                Console.ReadLine();
                Console.Clear();
                ActionController();
            }
        }


        public static void GetSAVENewCustomerInfo()
        {
            Console.WriteLine("You are about to add a new Customer to your Database!");
            Console.WriteLine(Line);
            Console.WriteLine("Please enter the New Customers information");

            //Get New Customers First Name
            Console.WriteLine("Please enter the Customer's First Name");
            string newCustFirstName = Console.ReadLine();

            //Get New Customers Last Name
            Console.WriteLine("Please Enter the Customer's Last Name");
            string newCustLastName = Console.ReadLine();

            //Get New Customers Address1
            Console.WriteLine("Please enter the Customers Address 1");
            string newCustAddress1 = Console.ReadLine();

            //Get New Customers Address2
            Console.WriteLine("Please enter the Customers Address 2 (if none, press enter)");
            string newCustAddress2 = Console.ReadLine();

            if (newCustAddress2 == null)
            {
                newCustAddress2 = " ";
            }

            //Get New Customers City
            Console.WriteLine("Please enter the Customers City");
            string newCustCity = Console.ReadLine();

            //Get New Customers State
            string newCustState = JeffToolBox.getClientState("Please enter the Customers State in 2 Characters ie: (CA   TX   MD)", true);

            //Get Zip Code
            decimal ZipCode = JeffToolBox.ReadDecimalZipCode("Please enter a 5-Digit Zip Code", true, false);
            string newCustZipCode = ZipCode.ToString();

            //Save new Customer in DB
            SaveNewCustomerinDB(newCustFirstName, newCustLastName, newCustAddress1, newCustAddress2, newCustCity, newCustState, newCustZipCode);

           

        }
        public static void SaveNewCustomerinDB(string firstName, string lastName, string address1, string address2, string city, string state, string zip)
        {
            //Access my Database
            using (var db = new LargeBankEntities())
            {
                try
                {
                    //Build Instance of Student Class/Table
                    Customer customer = new Customer();
                    customer.FirstName = firstName;
                    customer.LastName = lastName ;
                    customer.Address1 = address1;
                    customer.Address2 = address2;
                    customer.CreatedDate = DateTime.Now;
                    customer.City = city;
                    customer.States = state;
                    customer.Zip = zip;

                    //Add new Instance of Customer into DateBase
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    Console.WriteLine("Your New Customer has been added to the Database. \n Created on: {6} \n \n  Your Customers name is: {0} {1}. \n Address: {2} \n {7} \n City: {3} \n State: {4} \n Zip: {5}.", customer.FirstName, customer.LastName, customer.Address1, customer.City, customer.States, customer.Zip, customer.CreatedDate, customer.Address2);

                    //Go back to Main Menu
                    Console.WriteLine(Line);
                    Console.WriteLine("Press Enter to return to Main Menu...");
                    Console.ReadLine();
                    Console.Clear();
                    ActionController();
                }
                catch(Exception)
                {
                    Console.WriteLine("Your new Customer could not be saved. Please try again or contact System Admin.");
                }             
            }

        }

        public static void AddAccountsToCustomerPrompt()
        {
            Console.Clear();
            Console.WriteLine(Line);
            Console.WriteLine("Welcome to ADD ACCOUNT INFORMATION MENU \n \n");
            Console.WriteLine("Lord {0}", UserFirstName);
            Console.WriteLine("Would you like to: ");
            Console.WriteLine("1. Search by Specific Customer Name?  (Pick this option if you know the First and Last Name of the customer you would like to add Accoutns to.");
            Console.WriteLine("2. Choose which customer to add Accounts to from a list of All your Customers. ");
            Console.WriteLine("3. Go back to the MAIN MENU");
            Console.WriteLine("4. Exit Program");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    //Search from Specific Customer Name
                    Console.Clear();
                    AddAccountsToCustomerBySearch();
                    break;
                case "2":
                    Console.Clear();
                    AddAccountToCustomerByListing();  
                    break;
                case "3":
                    Console.Clear();
                    ActionController();
                    break;
                case "4":
                    Console.Clear();
                    ExitPrompt();
                    break;
                default:
                    Console.Clear();
                    AddAccountsToCustomerPrompt();
                    break;
            }
        }

        public static void AddAccountsToCustomerBySearch()
        {
            Console.Clear();
            Console.WriteLine(Line);
            Console.WriteLine("Lord {0}, \n \n ", UserFirstName);
            Console.WriteLine("Please enter the First Name of the customer of which you would like to add Accounts to");
            string SearchFirstName = Console.ReadLine();
            Console.WriteLine("Please enter the Last Name of the customer of which you would like to add Accounts to");
            string SearchLastName = Console.ReadLine();

            using (var db = new LargeBankEntities())
            {
                var customer = db.Customers.FirstOrDefault(c => c.FirstName == SearchFirstName && c.LastName == SearchLastName);

                if(customer != null)
                {
                    AddAccountsToCustomer(customer);
                    db.SaveChanges();

                    AddAccountsToCustomerPrompt();
                }
                else
                {
                    Console.WriteLine("Your search returned zero results");
                    Console.WriteLine("\n Press Enter to Continue");
                    Console.ReadLine();
                    AddAccountsToCustomerPrompt();

                }
                //Old Method
                //foreach (var customer in db.Customers)
                //{
                //    if(customer.FirstName == SearchFirstName && customer.LastName == SearchLastName)
                //    {
                //        AddAccountsToCustomer(customer);
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        //Build
                //        Console.WriteLine("Your search did not");

                //    }
                //}
            }
        }
        public static void AddAccountToCustomerByListing()
        {
            Console.Clear();
            Console.WriteLine(Line);
            Console.WriteLine("Lord {0}, \n \n ", UserFirstName);
            Console.WriteLine("Please select the Customer Number  Associated with the name of the customer you would like to add accounts to.");

            using (var db = new LargeBankEntities())
            {
                foreach (var customer in db.Customers)
                {
                    Console.WriteLine( customer.CustomerId + "). " + customer.FirstName +" " +customer.LastName +".");
                }


                  decimal ChosenCustomerDecimal = JeffToolBox.ReadDecimal("\n Enter the Customer Number you would like to Add Accounts to:", true, true);
                  int ChosenCustomerInt = (int)ChosenCustomerDecimal;

                var chosencustomer = db.Customers.Find(ChosenCustomerInt);

                if(chosencustomer == null)
                {
                    Console.WriteLine("Your search returned zero results");
                    Console.WriteLine("\n Press Enter to Continue");
                    Console.ReadLine();
                    AddAccountsToCustomerPrompt();
                }
                else
                {
                    AddAccountsToCustomer(chosencustomer);
                    db.SaveChanges();

                    AddAccountsToCustomerPrompt();
                }
               
                //foreach (var customer in db.Customers)
                //{
                //    if(ChosenCustomerInt == customer.CustomerId)
                //    {
                        
                //    }
                //    else
                //    {
                        

                //    }
                //}

            }


        }
        public static void AddAccountsToCustomer(Customer customer)
        {
            Console.Clear();
            Console.WriteLine(Line);
            Console.WriteLine("You are about to add an account to Customer Number {0}. Customer Name {1} {2} \n ",customer.CustomerId, customer.FirstName, customer.LastName);
                decimal myAccountNumberDecimal = JeffToolBox.ReadDecimal("Please enter the Account Number (Numbers ONLY) That you wish to add...", true, true);
                int myAccountNumber = (int)myAccountNumberDecimal;

                decimal myAccountBalanceDecimal = JeffToolBox.ReadDecimal("Please enter the Account balance using Numbers Only", true, true);
                int myAccountBalance = (int)myAccountBalanceDecimal;

                //Create a new Account
                var account = new Account();
                
                //Add Creation Date to My Account
                account.CreatedDate = DateTime.Now;

                //Add Account Number to account
                account.AccountNumber = myAccountNumber;

                //Add Balance to Account
                account.Balance = myAccountBalance;

                //Attach Accounts to Customer || Save to Database
                customer.Accounts.Add(account);       

                Console.WriteLine(Line);
                Console.WriteLine("You added Account Number {0} to Customer: {1} {2}.  \n The Account has a Balance of {3} ", account.AccountNumber, customer.FirstName, customer.LastName, account.Balance.ToString("C"));
                Console.WriteLine("\n \n Please press Enter to Return to the Add Account Information Menu.");
                Console.ReadLine();           
        }


        public static void ExitPrompt()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to exit? (yes/no)");
            string input = Console.ReadLine().ToLower();
            
            switch(input)
            {
                case "yes":
                case "y":
                    ExitProgram();
                    break;
                case "no":
                case "n":
                    ActionController();
                    break;
                        default:
                    ExitPrompt();
                    break;
            }
        }
        public static void ExitProgram()
        {
            Environment.Exit(0);
        }
    }
}
