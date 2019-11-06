using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCookbook.Data.Core.Repositories
{
    public interface IRepositoryBase<TEntity>
    {

        /// <summary>
        /// Get All <typeparamref name="TEntity"/> records
        /// </summary>
        /// <typeparam name="TEntity">The Entity type</typeparam>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Get All <typeparamref name="TEntity"/> records
        /// </summary>
        /// <param name="predicate">Filter expression</param>
        /// <typeparam name="TEntity">The Entity type</typeparam>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Get <typeparamref name="TEntity"/> record by Key value
        /// </summary>
        /// <param name="id">The Key value of the record</param>
        /// <typeparam name="TEntity">The Entity type</typeparam>
        /// <returns></returns>
        Task<TEntity> GetByKeyAsync(int id);

        /// <summary>
        /// Add new <typeparamref name="TEntity"/> record
        /// </summary>
        /// <param name="entity">The record data to add</param>
        /// <typeparam name="TEntity">The Entity type</typeparam>
        /// <returns></returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Update <typeparamref name="TEntity"/> record
        /// </summary>
        /// <param name="entity">The data to update the record with</param>
        /// <typeparam name="TEntity">The Entity type</typeparam>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete <typeparamref name="TEntity"/> record by Key value
        /// </summary>
        /// <param name="id">The Key value of the record</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
