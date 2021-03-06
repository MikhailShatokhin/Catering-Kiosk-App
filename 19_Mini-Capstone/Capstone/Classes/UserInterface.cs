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
<<<<<<< HEAD



        public void RunInterface()
        {
            

=======
        FileAccess fileAccess = new FileAccess();
        public void RunInterface()
        {
            fileAccess.CateringInventory();
>>>>>>> 655367ebf69042c2221d448bd133c44504a4c8e5
            bool done = false;
            while (!done)
            {
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
                    Console.WriteLine("(1) Add Money");
                    Console.WriteLine("(2) Select Products");
                    Console.WriteLine("(3) Complete Transaction");
                    Console.WriteLine("Current Account Balance: $" + CurrentBalance);
                    string userInput2 = Console.ReadLine();
                    if (userInput2 == "1")
                    {
                        AddMoney();
                    }
                    else if(userInput2 == "2")
                    {
<<<<<<< HEAD

                        DisplayInterface();

                        Console.WriteLine("Please enter a valid product ID.");

                        string productIdInput = Console.ReadLine();
                        SelectProducts(productIdInput);
=======
                        SelectProducts();
>>>>>>> 655367ebf69042c2221d448bd133c44504a4c8e5
                    }
                }
            }
        }

        private void DisplayInterface()
        {
            //Column Names&Output
            Console.WriteLine("Product Code" + "Description".PadLeft(24) + "Qty".PadLeft(24) + "Price".PadLeft(24));

            //Row Output
            //List<CateringItem> input = catering.CateringItems();
            //CateringItem[] items = catering.GetCateringItems(input);
            List<CateringItem> list = fileAccess.GetCateringItemList();
            foreach (CateringItem item in list)
                {
                    Console.WriteLine(" " + item.ProductId.PadRight(24) + item.Name + item.Quantity.ToString().PadLeft(34 - item.Name.Length) + "$".PadLeft(23 - item.Quantity.ToString().Length) + item.Price.ToString());
                }
        }

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
        private void SelectProducts()
        {
            try
            {
                DisplayInterface();
                //List<CateringItem> input = catering.CateringItems();
                //CateringItem[] items = catering.GetCateringItems(input);

                Console.WriteLine("Please enter a valid product ID.");

<<<<<<< HEAD
        private void SelectProducts(string productIdInput)
        {
            List<CateringItem> input = catering.CateringItems();
            CateringItem[] items = catering.GetCateringItems(input);

            foreach (CateringItem item in items)
            {
                if (Balance < item.Price)
                {
                    throw new Exception("Insufficient funds.");
                }
                //else if (Quantity.ToString() == "SOLD OUT")
                //{
                //    throw new Exception("Item not available.");
                //}
                else if (base.inventory.Contains(productIdInput))
                {
                    throw new Exception("Product not found.");
                }
                else if (productIdInput == item.ProductId)
                {
                    item.Quantity -= 1;
                    Balance -= item.Price;
                }
=======
                string productIdInput = Console.ReadLine();
                catering.SelectProducts(productIdInput,CurrentBalance);
                
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
>>>>>>> 655367ebf69042c2221d448bd133c44504a4c8e5
            }
        }

        
    }
}
