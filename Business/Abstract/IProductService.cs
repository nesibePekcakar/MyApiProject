﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>>GetAll();
        IDataResult<List<Product>>GetAllByCategory(int id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetByName(string name);

        IResult Add(Product product);
        IDataResult<Product> GetById(int id);
        IResult Delete(Product product);
        IResult Update(Product product);
        
    }
}
