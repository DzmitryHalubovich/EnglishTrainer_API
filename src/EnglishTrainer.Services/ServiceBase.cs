using EnglishTrainer.Contracts;
using EnglishTrainer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnglishTrainer.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class
    {
        private readonly EFContext _dbContext;

        public ServiceBase(EFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) => 
                    !trackChanges ? _dbContext.Set<T>().AsNoTracking() 
                                            :  _dbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
                    !trackChanges ? _dbContext.Set<T>().Where(expression).AsNoTracking()
                                            :  _dbContext.Set<T>().Where(expression);

        public void Create(T entity) => _dbContext.Set<T>().Add(entity);

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

    }
}
