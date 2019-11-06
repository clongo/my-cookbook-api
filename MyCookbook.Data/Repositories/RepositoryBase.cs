using Microsoft.EntityFrameworkCore;
using MyCookbook.Data.Contexts;
using MyCookbook.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCookbook.Data.Repositories
{
    /// <summary>
    /// This is the Repository Base class that contains all common crud operations for Entity Framework
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected RecipeContext RecipeContext { get; set; }

        public RepositoryBase(RecipeContext recipeContext)
        {
            this.RecipeContext = recipeContext;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await RecipeContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await RecipeContext.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByKeyAsync(int id)
        {
            //build expression to get generic entity by Id
            var keyProp = typeof(TEntity).GetProperties().Where(prop => prop.GetCustomAttributes(typeof(KeyAttribute), false).Any())
                .Select(prop => prop).FirstOrDefault();

            ParameterExpression parmExp = Expression.Parameter(typeof(TEntity));
            MemberExpression propertyExp = Expression.Property(parmExp, keyProp);
            ConstantExpression constantExp = Expression.Constant(id);

            Expression where = Expression.Equal(propertyExp, constantExp);
            Expression<Func<TEntity, bool>> predicate = Expression.Lambda<Func<TEntity, bool>>(where, parmExp);

            return await RecipeContext.Set<TEntity>().Where(predicate).AsNoTracking().FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task AddAsync(TEntity entity)
        {
            await RecipeContext.Set<TEntity>().AddAsync(entity);
            await RecipeContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(TEntity entity)
        {
            RecipeContext.Set<TEntity>().Update(entity);
            await RecipeContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            TEntity entity = await GetByKeyAsync(id);
            RecipeContext.Set<TEntity>().Remove(entity);
            await RecipeContext.SaveChangesAsync();
        }
    }
}
