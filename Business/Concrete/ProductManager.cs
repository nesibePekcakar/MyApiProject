using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Aspects.AutoFac;
using System.Linq;
using Core.Utilities.Business;
using Business.BusinessAspects;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }



        public IDataResult<List<Product>> GetAll()
        {
            //check access conditions
            //if the access cleared 
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);

        }

        public IDataResult<List<Product>> GetAllByCategory(int id)
        {
            var result = CheckCategoryExsists(id);
            if (!result.isSuccess)
            {
                return result;
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=> p.CategoryId==id));

        }

        public IDataResult<Product> GetById(int productId)
        {

            var result = CheckProductIdExsists(productId);
            if (!result.isSuccess)
            {
                return result;
            }
            var product = _productDal.Get(p => p.ProductId == productId);
            if (product == null)
            {
                return new ErrorDataResult<Product>(Messages.ProductDoesNotExist);
            }
            return new SuccessDataResult<Product>(product);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            var result = CheckPriceInterval(min, max);
            if (!result.isSuccess)
            {
                return result;
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            var productDetails = _productDal.GetProductDetails();

            if (productDetails == null || !productDetails.Any())
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.ProductDoesNotExsist);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        public IDataResult<Product> GetByName(string name)
        {
            var product = _productDal.Get(p => p.ProductName == name);

            if (product == null)
            {
                return new ErrorDataResult<Product>(Messages.ProductDoesNotExsist);
            }

            return new SuccessDataResult<Product>(product);
        }


        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //business codes if elses
            //categoride max 10
            IResult result = BusinessRules.Run(CheckCountCategory(product.CategoryId), CheckNameNonExsist(product.ProductName));
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [SecuredOperation("product.update,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            var result = CheckProductIdExsists(product.ProductId);
            if (result.isSuccess)
            {
                _productDal.Update(product);
                return new SuccessResult(Messages.ProductAdded);
            }
            return new ErrorResult();
           
        }
        [SecuredOperation("product.delete,admin")]
        public IResult Delete(Product product)
        {
            if (CheckNameDoesExsist(product.ProductName).isSuccess)
            {
                _productDal.Delete(product);
                return new SuccessResult(Messages.ProductDeleted);
            }
            return new ErrorResult();

        }


        //***********************************************************
        // Business rules for product
        //***********************************************************

        private IResult CheckCountCategory(int id) 
        {
            var count = _productDal.GetAll(p => p.CategoryId == id).Count;
            if (count >= 10)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult CheckNameNonExsist(string name)
        {
            var result = _productDal.GetAll(p => p.ProductName == name).Any();
            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult CheckNameDoesExsist(string name)
        {
            var result = _productDal.GetAll(p => p.ProductName == name).Any();
            if (result)
            {
                return new SuccessResult();
                
            }
            return new ErrorResult();
        }

        private IResult CheckDifferentCategpryNumber()
        {
            var result = _categoryService.GetAll().Data.Count;
            if (result>=15)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IDataResult<List<Product>> CheckCategoryExsists(int id)
        {
            var result = _productDal.GetAll(c => c.CategoryId == id);
            if (!result.Any())
            {
                return new ErrorDataResult<List<Product>>(Messages.CategoryNotFound);
            }
            return new SuccessDataResult<List<Product>>();
        }
        private IDataResult<Product> CheckProductIdExsists(int productId)
        {
            var result = _productDal.GetAll(c => c.ProductId == productId);
            if (!result.Any())
            {
                return new ErrorDataResult<Product>(Messages.ProductDoesNotExsist);
            }
            return new SuccessDataResult<Product>();
        }
        private IDataResult<List<Product>> CheckPriceInterval(decimal min, decimal max)
        {
            var result = _productDal.GetAll(p => p.UnitPrice <= max && p.UnitPrice >= min);
            if (!result.Any())
            {
                return new ErrorDataResult<List<Product>>(Messages.ProductDoesNotExsist);
            }
            return new SuccessDataResult<List<Product>>();
        }

    }
}
