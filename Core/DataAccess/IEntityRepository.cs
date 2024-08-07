﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;


namespace Core.DataAccess
{
    //Generic constraint to class referance type
    public interface IEntityRepository<T> where T : class,IEntitiy,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter= null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}
