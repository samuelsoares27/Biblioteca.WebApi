using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.WebApi
{
    public class Repository<T> : IRepository<T> where T : class

    {
        private readonly Context _context;

        public Repository(Context Context)
        {
            _context = Context;
        }

        public async Task<int> Add(T entity)
        {
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public Task<int> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;            
            return _context.SaveChangesAsync();
        }

        public Task<int> Delete(T entity)
        {
            _context.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
