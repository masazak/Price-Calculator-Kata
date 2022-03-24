using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello Mono World");
            IList<Product> product = Product.FillProduct();

            foreach (var pro in product)
            {
                Console.WriteLine("the book name = {0} ,UPC={1} ,price={2}" , pro.Name, pro.UPC, pro.Price);
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
                
                    Console.WriteLine("the book name = {0} ,UPC={1} ,price={2}" , prod.Name, prod.UPC, prod.Price);



                    Console.WriteLine("please enter tax percentage ");
                 string taxRead = Console.ReadLine();
                 decimal tax;
                if(string.IsNullOrWhiteSpace(taxRead))
                    tax = .2M;
                else
                 {   
                     string[] taxx = taxRead.Split('%');
                     tax = Decimal.Parse(taxx[0]) / 100;
                 }    
                

                calculateTax(tax, prod.Price);

            }


        }//main



        public static void calculateTax(decimal tax, decimal Price)
        {
            decimal total = Price + (Price * tax);

            Console.WriteLine("Product price reported as {0}  before tax and  {1} after {2} tax "
           , Price, System.Math.Round(total, 2), tax);

        }
    }
}
