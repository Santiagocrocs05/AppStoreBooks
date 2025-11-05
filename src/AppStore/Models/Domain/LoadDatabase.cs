using Microsoft.AspNetCore.Identity;

namespace AppStore.Models.Domain
{
    public class LoadDatabase
    {
        public static async Task InsertarData(DatabaseContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)

        {
            if (!roleManager.Roles.Any())
                await roleManager.CreateAsync(new IdentityRole("Admin"));


            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    Name = "Admin User",
                    Email = "scoronadovasco@gmail.com",
                    UserName = "scoronado"
                };

                await userManager.CreateAsync(user, "Ac6f1cf3.123..");
                await userManager.AddToRoleAsync(user, "Admin");
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                                    new Category { Name = "Drama" },
                                    new Category { Name = "Comedy" },
                                    new Category { Name = "Action" },
                                    new Category { Name = "Horror" }
                );
            }

            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book { Title = "100 años de soledad", Author = "Gabriel García Márquez", CreateDate = DateTime.Now, Imagen = "https://example.com/100anos.jpg" },
                    new Book { Title = "1984", Author = "George Orwell", CreateDate = DateTime.Now, Imagen = "https://example.com/1984.jpg" },
                    new Book { Title = "Harry Potter", Author = "J.K. Rowling", CreateDate = DateTime.Now, Imagen = "https://example.com/harrypotter.jpg" }

                );
            }

            if (!context.BookCategories.Any())
            {
                context.BookCategories.AddRange(
                    new BookCategory { BookId = 1, CategoryId = 1 },
                    new BookCategory { BookId = 2, CategoryId = 3 },
                    new BookCategory { BookId = 3, CategoryId = 2 }
                );
            }

            context.SaveChanges();
        }
    }
}