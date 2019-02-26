using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Core;

namespace TelephoneDirectory.BLL.Abstract
{
    public interface IBaseRepository<TEntity, TId, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        AppReturn Add(TEntity entity);
        AppReturn Update(TEntity entity);
        TEntity BringById(TId id);
        List<TEntity> ListAll(Expression<Func<TEntity, bool>> filter = null);
    }
}
