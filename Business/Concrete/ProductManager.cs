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

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //business codes if elses
            //categoride max 10
            IResult result =BusinessRules.Run(CheckCountCategory(product.CategoryId),CheckName(product.ProductName));
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            //check access conditions
            //if the access cleared 
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategory(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=> p.CategoryId==id));

        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=> p.ProductId == id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            if (CheckCountCategory(product.CategoryId).isSuccess)
            {
                _productDal.Update(product);
                return new SuccessResult(Messages.ProductAdded);
            }
            return new ErrorResult();
           
        }

        // Business rules
        private IResult CheckCountCategory(int id) 
        {
            var count = _productDal.GetAll(p => p.CategoryId == id).Count;
            if (count >= 10)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult CheckName(string name)
        {
            var result = _productDal.GetAll(p => p.ProductName == name).Any();
            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
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
    }
}
