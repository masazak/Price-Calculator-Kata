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
                
                Console.WriteLine("please select method to calculate discount, 1:additive ,2:multiplicative ");
                int number = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("please enter cap");
                decimal cap = ReadCapFromUser(prod.Price);
                decimal finalDiscount=0.0M;

                if(Product.UPCdiscount.Contains(upcBook))
                 {
                        decimal upcdiscount= .07M ;
                        decimal UPCdiscountAmount = calculateAmount(upcdiscount, prod.Price);
                        decimal totalDiscount=0.0M;
                        if(number == 1)
                        {
                            totalDiscount =discountamount + UPCdiscountAmount;
                         }
                        else if (number == 2)
                        {   
                            decimal discountTow = multiplicativeDiscounts(discountamount,prod.Price,upcdiscount);
                            totalDiscount =discountamount + discountTow ;
                        }
                        else
                        {
                            Console.WriteLine("please enter 1 or 2");
                            continue;
                        }
                        
                        if(cap <= totalDiscount)
                        { 
                            finalDiscount =cap;
                        }
                        else
                        {
                            finalDiscount=totalDiscount;
                        }

                        finalPrice = prod.Price + Taxamount - finalDiscount+packagingAmount+transport;

                         Console.WriteLine("cost ={0} , tax amount={1} , total discount={2} , Packaging amount={3} , transport={4} , total price={5} "
                           , prod.Price,Taxamount,finalDiscount,packagingAmount,transport,finalPrice);
                 }//if contains

              else
               {       if(cap <= discountamount)
                        { 
                            finalDiscount =cap;
                        }
                        else
                        {
                            finalDiscount=discountamount;
                        }
                        
                        Console.WriteLine("cost ={0} , tax amount={1} , total discount={2} , Packaging amount={3} , transport={4} , total price={5} "
                         , prod.Price,Taxamount,finalDiscount,packagingAmount,transport,finalPrice);
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

        public static decimal multiplicativeDiscounts(decimal discountamount,decimal Price,decimal upcdiscount)
        {
                decimal total = (Price - discountamount) * upcdiscount;
                return System.Math.Round(total, 2);
        }

        public static decimal ReadCapFromUser(decimal price)
         {
                string inputRead = Console.ReadLine();
                decimal read;
                if(inputRead.Contains( '$' ))
                {
                   string value=inputRead.Trim('$');
                    read=Decimal.Parse(value);
                }
                else
                {
                    string[] readd = inputRead.Split('%');
                    decimal num = Decimal.Parse(readd[0]) / 100;
                    read=price * num;
                }

                return read;
         }//ReadDiscountFromUser

        }
}
