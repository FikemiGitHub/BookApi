using Microsoft.EntityFrameworkCore;

namespace BookApi.Models
{
    public class BookContext: DbContext
    {
        public BookContext(DbContextOptions<BookContext> options): base(options)
        {
            Database.Migrate();
        }

        public DbSet<Book> Books { get; set; }
    }
}
