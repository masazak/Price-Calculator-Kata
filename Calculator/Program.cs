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
                decimal discount = ReadValueFromUser();

                Console.WriteLine("please enter packaging cost percentage ");
                decimal packaging = ReadValueFromUser();

                Console.WriteLine("please enter transport cost $ ");
                decimal transport = ReadTransportFromUser();

                decimal Taxamount = calculateAmount(tax, prod.Price);
                decimal discountamount = calculateAmount(discount, prod.Price);
                
                
                decimal packagingAmount=calculateAmount(packaging, prod.Price);

                decimal finalPrice=prod.Price + Taxamount - discountamount+packagingAmount+transport;
              
                    if(Product.UPCdiscount.Contains(upcBook))
                    {
                        decimal upcdiscount= .07M ;
                        decimal UPCdiscountAmount = calculateAmount(upcdiscount, prod.Price);

                        decimal totalDiscount =discountamount + UPCdiscountAmount;
                        finalPrice = prod.Price + Taxamount - totalDiscount+packagingAmount+transport;

                         Console.WriteLine("cost ={0} , tax amount={1} , total discount={2} , Packaging amount={3} , transport={4} , total price={5} "
                         , prod.Price,Taxamount,totalDiscount,packagingAmount,transport,finalPrice);
                    }//if contains

                    else
                    {
                        
                        Console.WriteLine("cost ={0} , tax amount={1} , total discount={2} , Packaging amount={3} , transport={4} , total price={5} "
                         , prod.Price,Taxamount,discountamount,packagingAmount,transport,finalPrice);
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


         public static decimal ReadValueFromUser()
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

        
         public static decimal ReadTransportFromUser()
         {
                string inputRead = Console.ReadLine();
                decimal read;
                if (string.IsNullOrWhiteSpace(inputRead))
                    read = 0.0M;
               else
                    read = Decimal.Parse(inputRead);

                return read;
         }//ReadTransportFromUser

         public static decimal calculateAmount(decimal amount, decimal Price)
         {
                decimal total = Price * amount;
                return System.Math.Round(total, 2);
         }

        }
}
