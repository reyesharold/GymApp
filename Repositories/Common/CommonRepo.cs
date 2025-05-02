using Azure;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Common
{
    public class CommonRepo<T> : ICommonRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _db;

        public CommonRepo(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<T> AddAync(T entity)
        {
            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> GetAsync(Expression<Func<T,bool>> condition,Func<IQueryable<T>,IQueryable<T>>? query = null)
        {
            IQueryable<T> db = _db;

            if(query != null)
            {
                db = query(db); 
            }

            var response = await db.FirstOrDefaultAsync(condition);

            return response!;
        }
        // query => query.Include(c => c.Sample)
        // query(Table);
        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>>? condition = null, Func<IQueryable<T>, IQueryable<T>>? query = null)
        {
            IQueryable<T> db = _db;

            if (query != null)
            {
                db = query(db);
            }

            if (condition != null)
            {
                db = db.Where(condition); 
            }

            var response = await db.ToListAsync();

            return response;
        }

        public async Task<bool> DeleteViaIdAsync(int id)
        {
            var item = await _db.FindAsync(id);
            if (item == null) { return false; }

            _db.Remove(item);

            return await _context.SaveChangesAsync() > 0; //SaveChangesAsync returns an integer if rows were affected
        }

        public async Task<T> UpdateAsync(T entity, params Expression<Func<T, object>>[] updateProperties)
        {
            _db.Attach(entity);

            foreach (var property in updateProperties)
            {
                _context.Entry(entity).Property(property).IsModified = true;
            }

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
