using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentLibrary.Data.Context;
using DocumentLibrary.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DocumentLibrary.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> DbSet;

        private readonly DocumentLibraryContext _context;

        public GenericRepository(DocumentLibraryContext context)
        {
            _context = context ?? throw new ArgumentException(
                "An instance of DbContext is required to use this repository.", nameof(context));

            DbSet = context.Set<T>();
        }

        public DocumentLibraryContext Context => _context;
        
        public virtual IQueryable<T> All()
            => DbSet;

        public virtual T GetById(Guid id) 
            => DbSet.Find(id);

        public virtual T GetById(long id)
            => DbSet.Find(id);

        public virtual T GetById(int id)
            => DbSet.Find(id);

        public virtual T GetById(string id)
            => DbSet.Find(id);
        
        public virtual async Task<T> GetByIdAsync(long id)
            => await DbSet.FindAsync(id);

        public virtual void Add(T entity)
            => DbSet.Add(entity);

        public virtual void AddRange(IEnumerable<T> entities)
            => DbSet.AddRange(entities);

        public virtual void Update(T entity) 
            => DbSet.Update(entity);
        
        public virtual void Delete(T entity) 
            => DbSet.Remove(entity);

        public void SaveChanges() 
            => _context.SaveChanges(); 

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void ChangeEntityState(T entity, EntityState state)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = state;
        }
    }
}