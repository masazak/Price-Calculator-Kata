using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            IList<Product> product = Product.FillProduct();

            foreach (var pro in product)
            {
                Console.WriteLine("the book name = {0} ,UPC={1} ,price={2}", pro.Name, pro.UPC, pro.Price);
            }

            while (true)
            {
                Console.WriteLine("please enter UPC of book");
                string upcRead = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(upcRead))
                {
                    Console.WriteLine("you must enter valid upc");
                    continue;

                }

                int upcBook = Convert.ToInt32(upcRead);
                var prod = Product.checkBook(product, upcBook);
                if (prod == null)
                {
                    Console.WriteLine("please enter valid upc");
                    continue;
                }

                Console.WriteLine("the book name = {0} ,UPC={1} ,price={2}", prod.Name, prod.UPC, prod.Price);

                Console.WriteLine("please enter tax percentage ");
                decimal tax = ReadFromUser();

                Console.WriteLine("please enter discount percentage ");
                decimal discount = ReadDiscountFromUser();

                decimal Taxamount = calculateAmount(tax, prod.Price);
                decimal discountamount = calculateAmount(discount, prod.Price);
                
                decimal finalPrice=prod.Price + Taxamount - discountamount;
              
                    if(Product.UPCdiscount.Contains(upcBook))
                    {
                        decimal upcdiscount= .07M ;
                        decimal UPCdiscountAmount = calculateAmount(upcdiscount, prod.Price);
                        decimal remainingPrice = prod.Price - UPCdiscountAmount;
                        
                        Taxamount =calculateAmount(tax, remainingPrice);
                        discountamount=calculateAmount(discount, remainingPrice);

                        decimal totalDiscount =discountamount + UPCdiscountAmount;
                        finalPrice = prod.Price + Taxamount - totalDiscount;

                         Console.WriteLine("tax={0} , discount = {1} ,upc-discount={2}, tax Amount={3},discount Amount={4} ,UPC Discount Amount={5} ,price={6} , totl discount Amount={7} "
                         , tax,discount,upcdiscount,Taxamount,discountamount,UPCdiscountAmount,finalPrice,totalDiscount);
                    }//if contains

                    else
                    {
                        
                        Console.WriteLine("tax={0} , discount = {1} , tax Amount={2},discount Amount={3} ,price={4} , totl discount Amount={5} "
                         , tax,discount,Taxamount,discountamount,finalPrice,discountamount);
                    }
              
                
            }
        }//main



         public static decimal ReadFromUser()
         {
                string inputRead = Console.ReadLine();
                decimal read;
                if (string.IsNullOrWhiteSpace(inputRead))
                    read = .2M;
                else
                {
                    string[] readd = inputRead.Split('%');
                    read = Decimal.Parse(readd[0]) / 100;
                }

                return read;
         }//ReadFromUser


         public static decimal ReadDiscountFromUser()
         {
                string inputRead = Console.ReadLine();
                decimal read;
                if (string.IsNullOrWhiteSpace(inputRead))
                    read = 0.0M;
                else
                {
                    string[] readd = inputRead.Split('%');
                    read = Decimal.Parse(readd[0]) / 100;
                }

                return read;
         }//ReadDiscountFromUser

         public static decimal calculateAmount(decimal amount, decimal Price)
         {
                decimal total = Price * amount;
                return System.Math.Round(total, 2);
         }

        }
}
