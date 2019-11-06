using Microsoft.EntityFrameworkCore;
using MyCookbook.Data.Contexts;
using MyCookbook.Data.Core.Models;
using MyCookbook.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyCookbook.Data.Repositories
{
    public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RecipeContext recipeContext)
            : base(recipeContext)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Recipe>> SearchRecipes(string userEmail, string searchTerms)
        {
            //construct search logic
            //each term with a space is a valid search term
            //recipes must contain anything in quotes in the search term

            //get quoted search
            string quote = String.Empty;
            string pattern = @"""(.*)""";
            foreach (Match m in Regex.Matches(searchTerms, pattern, RegexOptions.Compiled))
            {
                quote = m.Value;
                //just use the first one
                break;
            }

            //remove quote from search to further parse
            searchTerms = searchTerms.Replace($"\"{quote}\"", "");

            //get individual search terms
            string[] terms = searchTerms.Split(' ',',');

            //get results using terms
            var allRecipes = await GetAllAsync(r => r.UserEmail.Equals(userEmail));
            var results = allRecipes.Where(r => (r.Name != null && terms.All(t => r.Name.Contains(t))) //name has all terms
                                                || (r.Description != null && terms.All(t => r.Description.Contains(t))) //OR desctiption has all terms
                                                || (r.SiteName != null && terms.All(t => r.SiteName.Contains(t))) //OR site name has all terms
                                                || terms.All(t => r.Url.Contains(t)) //OR url has all terms
                                                ).ToList();

            //filter results by quoted text
            if(!String.IsNullOrEmpty(quote))
            {
                results = results.Where(r => (r.Name != null && r.Name.Contains(quote)) //name contains quote
                                        || (r.Description != null && r.Description.Contains(quote)) //OR desctiption contains quote
                                        || (r.SiteName != null && r.SiteName.Contains(quote)) //OR site name contains quote
                                        || r.Url.Contains(quote) //OR url contaions quote
                                        ).ToList();
            }

            return results;
        }

        /// <inheritdoc/>
        public async Task<Recipe> GetByUrlAndEmail(string recipeUrl, string userEmail)
        {
            return await RecipeContext.Recipes.FirstOrDefaultAsync(r => r.UserEmail == userEmail && r.Url == recipeUrl);
        }

        /// <inheritdoc/>
        public async Task UpdateRecipeByUrl(string url, Recipe recipe)
        {
            var dbRecipe = await GetByUrlAndEmail(url, recipe.UserEmail);

            //update values
            dbRecipe.Url = recipe.Url; //update the url incase of protocol difference in Google search
            dbRecipe.Description = recipe.Description;
            dbRecipe.Image = recipe.Image;
            dbRecipe.Name = recipe.Name;
            dbRecipe.RatingCount = recipe.RatingCount;
            dbRecipe.RatingValue = recipe.RatingValue;
            dbRecipe.SiteName = recipe.SiteName;
            dbRecipe.TotalTime = recipe.TotalTime;

            await UpdateAsync(dbRecipe);
        }

        /// <inheritdoc/>
        public async Task DeleteByUrlAndEmail(string recipeUrl, string userEmail)
        {
            var recipe = await GetByUrlAndEmail(recipeUrl, userEmail);
            RecipeContext.Recipes.Remove(recipe);
            await RecipeContext.SaveChangesAsync();
        }
    }
}
