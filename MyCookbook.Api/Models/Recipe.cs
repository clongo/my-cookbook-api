using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCookbook.Api.Models
{
    public class Recipe
    {
        public string Url { get; set; }
        public string SiteName { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string RatingValue { get; set; }
        public string RatingCount { get; set; }
        public string TotalTime { get; set; }
    }
}
