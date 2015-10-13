﻿using LargeBank_ORM.Classes;
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
            GetSAVENewCustomerInfo();
           
        
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

        public static void GetSAVENewCustomerInfo()
        {
            Console.WriteLine("You are about to add a new Customer to your Database!");
            Console.WriteLine(Line);
            Console.WriteLine("Please enter the New Customers information");

            //Get New Customers First Name
            Console.WriteLine("Please enter the Customer's First Name");
            string newCustFirstName = Console.ReadLine();

            //Get New Customers Lasst Name
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
                }
                catch(Exception)
                {
                    Console.WriteLine("Your new Customer could not be saved. Please try again or contact System Admin.");
                }
            }

        }

    }
}
