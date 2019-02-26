using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.BLL.Abstract;
using TelephoneDirectory.Core;

namespace TelephoneDirectory.BLL.Concrete
{
    public class BaseRepository<TEntity, TId, TContext> : IBaseRepository<TEntity, TId, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        } 

        private IDbSet<TEntity> DbSet => _context.Set<TEntity>();
        public AppReturn Add(TEntity entity)
        {
            try
            {
                DbSet.Add(entity);
                return AppReturn.Successful();
            }
            catch (Exception e)
            {
                // TODO: Log configuration must add here.
                return AppReturn.InvalidOperation(e.Message);
            }
        }
        public TEntity BringById(TId id)
        {
            return DbSet.Find(id);
        }
        public List<TEntity> ListAll(Expression<Func<TEntity, bool>> filter = null)
        {
            List<TEntity> list;

            if (filter == null)
            {
                list = DbSet.ToList();
            }
            else
            {
                list = DbSet.Where(filter).ToList();
            }

            return list;
        }
        public AppReturn Update(TEntity entity)
        {
            try
            {
                DbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return AppReturn.Successful();
            }
            catch (Exception e)
            {
                // TODO: Log configuration must add here.
                return AppReturn.InvalidOperation(e.Message);
            }
        }
    }
}
