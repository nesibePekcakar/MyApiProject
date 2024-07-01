using Entities.Concrete;
using Business.Concrete;
using DataAccess.Concrete;
using System;
using DataAccess.Concrete.InMemory;
using DataAccess.Concrete.EntitiyFramework;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductTest();
            //CategoryTest();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }
        



        private static void ProductTest()
        {
        ProductManager productManager = new ProductManager(new EfProductDal());
        Console.WriteLine("Hello World!");
            var result = productManager.GetProductDetails();
            if (result.isSuccess)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                }
            }
            else 
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}
    

