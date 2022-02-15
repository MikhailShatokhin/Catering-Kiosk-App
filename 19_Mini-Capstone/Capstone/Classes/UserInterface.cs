using Capstone.Classes.CateringItemsSubclasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface : Catering
    {
        // This class provides all user communications, but not much else.
        // All the "work" of the application should be done elsewhere

        // ALL instances of Console.ReadLine and Console.WriteLine should 
        // be in this class.
        // NO instances of Console.ReadLine or Console.WriteLIne should be
        // in any other class.

        private Catering catering = new Catering();
        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                //Main Menu
                Console.WriteLine("(1) Display Catering Items");
                Console.WriteLine("(2) Order");
                Console.WriteLine("(3) Quit");
                string userInput = Console.ReadLine();
                Console.WriteLine();
                
                if(userInput=="1")
                {
                    DisplayInterface();
                }
                else if(userInput=="2")
                {
                    //Sub Menu
                    Console.WriteLine("(1) Add Money");
                    Console.WriteLine("(2) Select Products");
                    Console.WriteLine("(3) Complete Transaction");
                    Console.WriteLine("Current Account Balance: $" + catering.CurrentBalance);
                    string userInput2 = Console.ReadLine();
                    if (userInput2 == "1")
                    {
                        AddMoney();
                    }
                    else if(userInput2 == "2")
                    {
                        SelectProducts();
                    }
                    else if (userInput2 == "3")
                    {
                        CompleteTransaction();
                    }
                }
                else if (userInput == "3")
                {
                    Console.WriteLine("Thank you!");
                    done = true;
                }
            }
        }

        //Void DisplayInterface Method
        private void DisplayInterface()
        {
            //Column Names
            Console.WriteLine("  Product Code" + "Description".PadLeft(16) + "Qty".PadLeft(16) + "Price".PadLeft(13));

            //Row Output
            foreach (CateringItem item in catering.items)
            {
                if(item.Quantity<=0)
                {
                    Console.WriteLine("   " + item.ProductId.PadRight(16) + item.Name + item.Quantity.ToString("SOLD OUT").PadLeft(32 - item.Name.Length) + "$".PadLeft(5 - item.Quantity.ToString().Length) + item.Price.ToString());
                }
                else
                {
                    Console.WriteLine("   " + item.ProductId.PadRight(16) + item.Name + item.Quantity.ToString().PadLeft(26 - item.Name.Length) + "$".PadLeft(12 - item.Quantity.ToString().Length) + item.Price.ToString());
                }
            }
        }

        //Void AddMoney Method
        private void AddMoney()
        {
                try
                {
                    Console.WriteLine("Please enter a $1, $5, $10, $20, $50 or $100 bill.");
                    int deposit = int.Parse(Console.ReadLine());

                    decimal balance = catering.AddMoney(deposit);
                    CurrentBalance = balance;
                    Console.WriteLine("$" + CurrentBalance);
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
        }

        //Void SelectProducts Method
        private void SelectProducts()
        {
            try
            {
                DisplayInterface();               

                Console.WriteLine("Please enter a valid product ID.");
                string productIdInput = Console.ReadLine();

                Console.WriteLine("Input the quantity.");
                int quantity = int.Parse(Console.ReadLine());

                catering.SelectProducts(productIdInput, quantity);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }
        }

        //Void CompleteTransaction Method
        private void CompleteTransaction()
        {
            catering.CompleteTransaction();
            List<string> lines = catering.ReceiptPrinter();
            foreach (string line in lines)
            {
                string[] newLines = line.Split("|");
                string note = catering.GetNotes(newLines);
                Console.WriteLine(newLines[0].PadLeft(3) + " " + newLines[1] + " " + newLines[2].PadLeft(27 - newLines[1].Length) + " " + newLines[3].PadLeft(10) + " " + newLines[4].PadLeft(10) + "  " + note);
            }
            Console.WriteLine(catering.GetTotal());
            Console.WriteLine(catering.PrintChange());
        }
    }
}
