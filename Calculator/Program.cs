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

                Console.WriteLine("enter currency the defult is USD");
                string codename = Console.ReadLine();
                decimal code=ChooseCurrency(codename);
                decimal productprice=prod.Price*code;
                Console.WriteLine("the book name =" +prod.Name + "UPC= "+prod.UPC+ "price= "+productprice + codename );

                Console.WriteLine("please enter tax percentage ");
                decimal tax = ReadFromUser();

                Console.WriteLine("please enter discount percentage ");
                decimal discount = ReadValueFromUser();

                Console.WriteLine("please enter packaging cost percentage ");
                decimal packaging = ReadValueFromUser();

                Console.WriteLine("please enter transport cost $ ");
                decimal transport = ReadTransportFromUser();

                decimal Taxamount = calculateAmount(tax, productprice);
                decimal discountamount = calculateAmount(discount, productprice);
                decimal packagingAmount=calculateAmount(packaging, productprice);

                decimal finalPrice=productprice + Taxamount - discountamount+packagingAmount+transport;
                
                Console.WriteLine("please select method to calculate discount, 1:additive ,2:multiplicative ");
                int number = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("please enter cap");
                decimal cap = ReadCapFromUser(productprice);
                decimal finalDiscount=0.0M;

                if(Product.UPCdiscount.Contains(upcBook))
                 {
                        decimal upcdiscount= .07M ;
                        decimal UPCdiscountAmount = calculateAmount(upcdiscount, productprice);
                        decimal totalDiscount=0.0M;
                        if(number == 1)
                        {
                            totalDiscount =discountamount + UPCdiscountAmount;
                         }
                        else if (number == 2)
                        {   
                            decimal discountTow = multiplicativeDiscounts(discountamount,productprice,upcdiscount);
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

                        finalPrice = productprice+ Taxamount - finalDiscount+packagingAmount+transport;

                         Console.WriteLine("cost ={0} , tax amount={1} , total discount={2} , Packaging amount={3} , transport={4} , total price={5} "
                           , productprice,System.Math.Round(Taxamount,2),System.Math.Round(finalDiscount,2),System.Math.Round(packagingAmount,2),System.Math.Round(transport,2),System.Math.Round(finalPrice,2));
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
                         , productprice,System.Math.Round(Taxamount,2),System.Math.Round(finalDiscount,2),System.Math.Round(packagingAmount,2),System.Math.Round(transport,2),System.Math.Round(finalPrice,2));
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
                return System.Math.Round(total, 4);
         }

        public static decimal multiplicativeDiscounts(decimal discountamount,decimal Price,decimal upcdiscount)
        {
                decimal total = (Price - discountamount) * upcdiscount;
                return System.Math.Round(total, 4);
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
        public static decimal ChooseCurrency(string code)
        {
            switch (code.ToUpper())
            {
                case "USD":
                    return 1M;
                    break;
                case "GBP":
                    return .82M;
                    break;
                case "JPY":
                    return 128.40M;
                    break;
                default:
                    return 1;
                    break;
            }
        }
       
}
}
