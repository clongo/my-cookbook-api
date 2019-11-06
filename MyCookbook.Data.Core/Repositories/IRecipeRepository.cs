using MyCookbook.Data.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCookbook.Data.Core.Repositories
{
    public interface IRecipeRepository : IRepositoryBase<Recipe>
    {
        Task<Recipe> GetByUrlAndEmail(string recipeUrl, string userEmail);
        Task UpdateRecipeByUrl(string url, Recipe recipe);
        Task DeleteByUrlAndEmail(string recipeUrl, string userEmail);
    }
}
