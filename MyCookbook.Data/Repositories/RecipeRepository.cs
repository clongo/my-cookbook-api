using Microsoft.EntityFrameworkCore;
using MyCookbook.Data.Contexts;
using MyCookbook.Data.Core.Models;
using MyCookbook.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCookbook.Data.Repositories
{
    public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RecipeContext recipeContext)
            : base(recipeContext)
        {
        }

        public async Task<Recipe> GetByUrlAndEmail(string recipeUrl, string userEmail)
        {
            return await RecipeContext.Recipes.FirstOrDefaultAsync(r => r.UserEmail == userEmail && r.Url == recipeUrl);
        }

        public async Task UpdateRecipeByUrl(string url, Recipe recipe)
        {
            var dbRecipe = await GetByUrlAndEmail(url, recipe.UserEmail);

            //update values
            dbRecipe.Url = recipe.Url;
            dbRecipe.Description = recipe.Description;
            dbRecipe.Image = recipe.Image;
            dbRecipe.Name = recipe.Name;
            dbRecipe.RatingCount = recipe.RatingCount;
            dbRecipe.RatingValue = recipe.RatingValue;
            dbRecipe.SiteName = recipe.SiteName;
            dbRecipe.TotalTime = recipe.TotalTime;

            await UpdateAsync(dbRecipe);
        }

        public async Task DeleteByUrlAndEmail(string recipeUrl, string userEmail)
        {
            var recipe = await GetByUrlAndEmail(recipeUrl, userEmail);
            RecipeContext.Recipes.Remove(recipe);
            await RecipeContext.SaveChangesAsync();
        }
    }
}
