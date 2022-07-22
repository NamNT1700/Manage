using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Manage.Model.Base;

namespace Manage.Repository.Base.IRepository
{
    public interface IRepositoryBase<T>where T : class, IEntityBase
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression);
        string CheckNull(string code,int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);

    }
}
