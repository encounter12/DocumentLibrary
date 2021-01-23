using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentLibrary.Data.Repositories.Contracts
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> All();

        T GetById(Guid id);

        T GetById(long id);

        T GetById(int id);

        T GetById(string id);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        void SaveChanges();
    }
}