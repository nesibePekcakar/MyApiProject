using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {   //Database simulation for learning purposes
            _products = new List<Product>
            {
                new Product{CategoryId=1,ProductId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
                new Product{CategoryId=2,ProductId=2,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
                new Product{CategoryId=3,ProductId=3,ProductName="Telefon",UnitPrice=1500,UnitsInStock=15},
                new Product{CategoryId=4,ProductId=4,ProductName="Klavye",UnitPrice=150,UnitsInStock=65}
            }; 
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //LINQ - Language Integrated Query
            Product toDel = _products.SingleOrDefault(p=>p.ProductId == product.ProductId);
            _products.Remove(toDel);
        }

        public Product Get(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
             return _products.Where(p=> p.CategoryId == categoryId).ToList();
           
            
        }
        public List<ProductDetailDto> GetProductDetails(){

            throw new NotImplementedException();
        }


        public void Update(Product product)
        {
            Product toUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            toUpdate.ProductName = product.ProductName;
            toUpdate.CategoryId = product.CategoryId;
            toUpdate.UnitPrice = product.UnitPrice;
            toUpdate.UnitsInStock = product.UnitsInStock;
            
        }
    }
}
