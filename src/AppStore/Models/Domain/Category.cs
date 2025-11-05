using System.ComponentModel.DataAnnotations;

namespace AppStore.Models.Domain
{
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Book> bookRelationList { get; set; }
        public virtual ICollection<BookCategory> BookCategoryRelationList { get; set; }
    }
}