using Capstone.Classes.CateringItemsSubclasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class Catering //: IComparable
    {
        //This class should contain all the "work" for catering

        //Objects
        private FileAccess fileAccess = new FileAccess();
        public List<CateringItem> items = new List<CateringItem>();

        //Constructors
        public Catering()
        {
            items = fileAccess.CateringInventory();
        }
        //public string CompareTo(Catering items)
        //{
        //    return items.ToString();
        //}

        //Add Money Method
        public decimal CurrentBalance { get; set; } = 0.00M;
        public decimal AddMoney(decimal deposit)
        {
            if (deposit == 1 || deposit == 5 || deposit == 10 || deposit == 20 || deposit == 50 || deposit == 100)
            {
                CurrentBalance += deposit;
                if (CurrentBalance > 1500.00M)
                {
                    throw new IndexOutOfRangeException("Balance cannot exceed $1500.");
                }
            }
            else
            {
                throw new IndexOutOfRangeException("Not a valid bill.");
            }
            fileAccess.AddMoneyLog(deposit, CurrentBalance); //Log Output

            return CurrentBalance;
        }

        //SelectProducts & Subtract from Balance Method
        public Dictionary<string, int> shoppingCart = new Dictionary<string, int>();
        public decimal SelectProducts(string productIdInput, int quantity)
        {         
            bool idValidity = false;
            foreach (CateringItem item in items)
            {
                foreach (CateringItem item2 in items)
                {
                    if (item2.ProductId == productIdInput)
                    {
                        idValidity = true;
                    }
                }
                if(idValidity==false)
                {
                    throw new Exception("Product not found.");
                }
                if (item.ProductId == productIdInput)
                {
                    if (CurrentBalance < (item.Price * quantity))
                    {
                        throw new Exception("Insufficient funds.");
                    }
                    else if (item.Quantity.ToString() == "SOLD OUT")
                    {
                        throw new Exception("Item not available.");
                    }
                    else
                    {
                        shoppingCart.Add(item.ProductId, quantity);
                        CurrentBalance -= (item.Price * quantity);
                    }
                }
            }
            return CurrentBalance;
        }

        //Complete Transactions Method
        public List<CateringItem> CompleteTransaction()
        {
            foreach (KeyValuePair<string, int> kvp in shoppingCart)
            {
                foreach (CateringItem item in items)
                {
                    if (item.ProductId == kvp.Key)
                    {
                        item.Quantity -= kvp.Value;
                    }
                }
            }
            return items;
        }
        
        //Get Notes Method
        public string GetNotes(string [] input)
        {
            string note = "";
            if(input[1].Substring(0, 1) == "A")
            {
                note = "You might need extra plates.";
            }
            if (input[1].Substring(0, 1) == "B")
            {
                note = "Don't forget ice.";
            }
            if (input[1].Substring(0, 1) == "D")
            {
                note = "Coffee goes with dessert.";
            }
            if (input[1].Substring(0, 1) == "E")
            {
                note = "Did you remember dessert?";
            }
                return note;
        }

        //Receipt Printer Method
        public List<string> ReceiptPrinter()
        {
            List<string> lines = new List<string>();
            {
                string output = "";
                foreach (KeyValuePair<string, int> kvp in shoppingCart)
                {
                    foreach (CateringItem item in items)
                    {
                        if (kvp.Key.Substring(0, 2) == item.ProductId)
                        {
                            string typeOfFood = "";
                            if (kvp.Key.Substring(0, 1) == "A")
                            {
                                typeOfFood = "Appetizer";
                            }
                            else if (kvp.Key.Substring(0, 1) == "B")
                            {
                                typeOfFood = "Beverage";
                            }
                            else if (kvp.Key.Substring(0, 1) == "D")
                            {
                                typeOfFood = "Dessert";
                            }
                            else if (kvp.Key.Substring(0, 1) == "E")
                            {
                                typeOfFood = "Entree";
                            }
                            output = kvp.Value + "|" + typeOfFood + "|" + item.Name + "|" + "$" + item.Price + "|" + "$" + (item.Price * kvp.Value);
                            lines.Add(output);
                            fileAccess.OrderLog(kvp.Value, item.Name, item.ProductId, item.Price * kvp.Value, CurrentBalance); //Log Output
                        }  
                    }
                }
            }
            return lines;
        }

        //Get Total Method
        public string GetTotal()
        { 
            decimal total = 0;
            List<string> lines = new List<string>();
            {
                foreach (KeyValuePair<string, int> kvp in shoppingCart)
                {
                    foreach (CateringItem item in items)
                    {
                        if (kvp.Key.Substring(0, 2) == item.ProductId)
                        {
                            total += (kvp.Value * item.Price);
                        }
                    }
                }
            }
            string output = "Total: $" + total;
            return output;
        }

        //Print Change Method
        public string PrintChange()
        {
            decimal returnBalance = CurrentBalance;
            int nickels = 0;
            int dimes = 0;
            int quarters = 0;
            int ones = 0;
            int fives = 0;
            int tens = 0;
            int twenties = 0;
            int fifties = 0;

            decimal coinMoney = CurrentBalance;
            coinMoney = ((CurrentBalance - (int)(CurrentBalance)) * 100);

            //Change calc
            if ((int)coinMoney >= 25)
            {
                quarters = ((int)coinMoney / 25);
                coinMoney -= (quarters * 25);
            }
            if ((int)coinMoney >= 10)
            {
                dimes = ((int)coinMoney / 10);
                coinMoney -= (dimes * 10);
            }
            if ((int)coinMoney >= 5)
            {
                nickels = ((int)coinMoney / 5);
                coinMoney -= (nickels * 5);
            }

            //Dollar calc
            if (CurrentBalance >= 50.00M)
            {
                fifties = (int)(CurrentBalance / 50);
                CurrentBalance -= (fifties * 50);
            }
            if (CurrentBalance >= 20.00M)
            {
                twenties = (int)(CurrentBalance / 20);
                CurrentBalance -= (twenties * 20);
            }
            if (CurrentBalance >= 10.00M)
            {
                tens = (int)(CurrentBalance / 10);
                CurrentBalance -= (tens * 10);
            }
            if (CurrentBalance >= 5.00M)
            {
                fives = (int)(CurrentBalance / 5);
                CurrentBalance -= (fives * 5);
            }
            if (CurrentBalance >= 1.00M)
            {
                ones = (int)(CurrentBalance / 1);
                CurrentBalance -= (ones * 1);
            }       
         
            //String Concatenation
            string change = "You received";
            if (fifties != 0)
            {
                change += " (" + fifties + ") Fifties";
            }
            if (twenties != 0)
            {
                change += " (" + twenties + ") Twenties";
            }
            if (tens != 0)
            {
                change += " (" + tens + ") Tens";
            }
            if (fives != 0)
            {
                change += " (" + fives + ") Fives";
            }
            if (ones != 0)
            {
                change += " (" + ones + ") Ones";
            }
            if (quarters != 0)
            {
                change += " (" + quarters + ") Quarters";
            }
            if (dimes != 0)
            {
                change += " (" + dimes + ") Dimes";
            }
            if (nickels != 0)
            {
                change += " (" + nickels + ") Nickels";
            }
            change += " in change.";
            fileAccess.ChangeLog(returnBalance); //Log Output
            return change;
        }
    }
}
