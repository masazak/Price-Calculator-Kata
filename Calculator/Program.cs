using System;
using System.Collections.Generic;

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
                decimal discount = ReadFromUser();

                decimal Taxamount = calculateAmount(tax, prod.Price);

                decimal discountamount = calculateAmount(discount, prod.Price);
                decimal finalPrice = prod.Price + Taxamount - discountamount;
                Console.WriteLine("tax={0} , discount={1} , taxamount={2} , discountamount={3} , price before={4} ,  price after={5}  "
               , tax, discount, Taxamount, discountamount, prod.Price, finalPrice);
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

            public static decimal calculateAmount(decimal amount, decimal Price)
            {
                decimal total = Price * amount;
                return System.Math.Round(total, 2);
            }

        }
}
