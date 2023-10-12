using System.Linq.Expressions;

namespace FarmBank.Integration.Mongo.Interfaces;

public interface IDbSet<TEntity>
{
    IQueryable<TEntity> AsQueryable();

    Task<IEnumerable<TEntity>> FilterByAsync(
        Expression<Func<TEntity, bool>> filterExpression);

    //IEnumerable<TProjected> FilterBy<TProjected>(
    //    Expression<Func<TEntity, bool>> filterExpression,
    //    Expression<Func<TEntity, TProjected>> projectionExpression);

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filterExpression);

    Task<TEntity> FindByIdAsync(Guid id);

    ValueTask<long> InsertAsync(TEntity document);

    ValueTask<long> InsertAsync(IEnumerable<TEntity> documents);

    ValueTask<long> ReplaceAsync(TEntity document);

    ValueTask<long> ReplaceAsync(IEnumerable<TEntity> documents);

    ValueTask<long> DeleteWhereAsync(Expression<Func<TEntity, bool>> filterExpression);

    ValueTask<long> DeleteAsync(Guid id);

    ValueTask<long> DeleteAsync(IEnumerable<Guid> ids);
}