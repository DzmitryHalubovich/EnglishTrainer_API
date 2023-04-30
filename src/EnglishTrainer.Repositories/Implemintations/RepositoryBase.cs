using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnglishTrainer.Repositories.Implemintations
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly EFContext _dbContext;

        public RepositoryBase(EFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
                    !trackChanges ? _dbContext.Set<T>().AsNoTracking()
                                            : _dbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
                    !trackChanges ? _dbContext.Set<T>().Where(expression).AsNoTracking()
                                            : _dbContext.Set<T>().Where(expression);

        public void Create(T entity) => _dbContext.Set<T>().Add(entity);

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

    }
}
