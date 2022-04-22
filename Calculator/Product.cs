using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Product
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public decimal Price { get; set; }

        public static  int[] UPCdiscount =new int[] {12345,12346};

        public static IList<Product> FillProduct()
        {

            IList<Product> product = new List<Product>(){
         new Product(){Name="The Prince",UPC=12345,Price=20.25M },
         new Product(){Name="The Kingdom",UPC=12346,Price=30.25M },
         new Product(){Name="Paris",UPC=12347,Price=7.44m },
         new Product(){Name="secret of life",UPC=12348,Price=19.25M},
         new Product(){Name="happiness",UPC=12349,Price=10.8M }
        };

            return product;
        }//fillproduct

        public static Product checkBook(IList<Product> product, int upcBook)
        {
            foreach (var pro in product)
            {
                if (pro.UPC == upcBook)
                {
                    return pro;
                }
            }//foreach
            return null;
        }//checkBook
    }
}
