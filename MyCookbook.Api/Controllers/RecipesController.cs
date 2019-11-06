using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCookbook.Data.Core.Models;
using MyCookbook.Data.Core.Repositories;

namespace MyCookbook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository recipeRepository;
        public RecipesController(IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }

        /// <summary>
        /// Get All saved recipes for the logged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Recipe>> Get()
        {
            var email = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "email").Value;

            return await recipeRepository.GetAllAsync(r => r.UserEmail == email);
        }

        /// <summary>
        /// Get saved recipes for the logged in user that match searchTerm
        /// </summary>
        /// <returns></returns>
        [HttpGet("Search")]
        public async Task<IEnumerable<Recipe>> Search(string searchTerms)
        {
            var email = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "email").Value;

            return await recipeRepository.SearchRecipes(email, searchTerms);
        }

        /// <summary>
        /// Save a recipe for the logged in user
        /// </summary>
        /// <param name="recipe">The Recipe to Save</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Recipe> Put(Recipe recipe)
        {
            var email = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "email").Value;
            recipe.UserEmail = email;
            await recipeRepository.AddAsync(recipe);
            return recipe;
        }

        /// <summary>
        /// Update a Recipe for a logged in user
        /// </summary>
        /// <param name="recipe">The Recipe to update</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Recipe> Post(string url, Recipe recipe)
        {
            var email = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "email").Value;
            recipe.UserEmail = email;
            await recipeRepository.UpdateRecipeByUrl(url, recipe);
            return recipe;
        }

        /// <summary>
        /// Remove a recipe matching the url for the logged in user
        /// </summary>
        /// <param name="url">The url of the recipe to delete</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task Delete(string url)
        {
            var email = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "email").Value;
            await recipeRepository.DeleteByUrlAndEmail(url, email);
        }
    }
}