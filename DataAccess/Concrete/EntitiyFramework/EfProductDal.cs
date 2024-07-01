using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntitiyFramework
{
    //We need NuGet to implement DataAccess
    public class EfProductDal : EfEntityRepositoryBase<Product,MssqlContext>,IProductDal
    {
        public void Add(Product entity)
        {
            using (MssqlContext context = new MssqlContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                context.SaveChanges();

            }
        }

        public void Delete(Product entity)
        {
            using (MssqlContext context = new MssqlContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (MssqlContext context = new MssqlContext())
            {
                try
                {
                    return context.Set<Product>().SingleOrDefault(filter);
                }
                catch (Exception ex) {

                    throw new Exception("Error while retrieving product", ex);

                }
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (MssqlContext context = new MssqlContext())
            {
                try
                {
                    return filter == null ? context.Set<Product>().ToList()
                        : context.Set<Product>().Where(filter).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");

                    throw new Exception("Error while retrieving product", ex);

                }
            }
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            using (MssqlContext context = new MssqlContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId=p.ProductId,
                                 ProductName=p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock= p.UnitsInStock              
                             };
                return result.ToList();
            }
           
        }

        public void Update(Product entity)
        {
            using (MssqlContext context = new MssqlContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}

