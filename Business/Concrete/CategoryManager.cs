using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntitiyFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICatagoryDal _categoryDal;

        public CategoryManager(ICatagoryDal categoryDal)
        {

            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(), Messages.ProductListed);
        }

        public DataResult<Category> GetById(int id)
        {

            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == id));
        }
    }
}
