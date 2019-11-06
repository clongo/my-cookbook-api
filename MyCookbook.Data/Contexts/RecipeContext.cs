using Microsoft.EntityFrameworkCore;
using MyCookbook.Data.Core.Models;

namespace MyCookbook.Data.Contexts
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
