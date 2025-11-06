using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Models.Domain
{
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Imagen { get; set; }
        [Required]
        public string? Author { get; set; }

        public virtual ICollection<Category>? categoryRelationList { get; set; }
        public virtual ICollection<BookCategory>? BookCategoryRelationList { get; set; }

        [NotMapped]
        public List<int>? Categories { get; set; }

        [NotMapped]
        public string? CategoriesName { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? CategoriesList { get; set; }
        [NotMapped]
        public MultiSelectList? MulticategoriesList { get; set; }
    }
}