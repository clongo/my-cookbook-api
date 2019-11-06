using MyCookbook.Data.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCookbook.Data.Core.Repositories
{
    public interface IRecipeRepository : IRepositoryBase<Recipe>
    {
        /// <summary>
        /// Search User recipes using the google search term
        /// </summary>
        /// <param name="userEmail">The Logged in User Email</param>
        /// <param name="searchTerms">The search terms to use</param>
        /// <returns></returns>
        Task<IEnumerable<Recipe>> SearchRecipes(string userEmail, string searchTerms);

        /// <summary>
        /// Get a Saved recipe by the Url for the given user
        /// </summary>
        /// <param name="recipeUrl">The Url of the recipe to return</param>
        /// <param name="userEmail">The Logged in User Email</param>
        /// <returns></returns>
        Task<Recipe> GetByUrlAndEmail(string recipeUrl, string userEmail);

        /// <summary>
        /// Update the Saved Recipe for the given url
        /// </summary>
        /// <param name="url">The Url of the recipe to update</param>
        /// <param name="recipe">The Recipe data to save</param>
        /// <returns></returns>
        Task UpdateRecipeByUrl(string url, Recipe recipe);

        /// <summary>
        /// Remove the Saved recipe by Url for the given user
        /// </summary>
        /// <param name="recipeUrl">The Url of the recipe to remove</param>
        /// <param name="userEmail">The Logged in User Email</param>
        /// <returns></returns>
        Task DeleteByUrlAndEmail(string recipeUrl, string userEmail);
    }
}
