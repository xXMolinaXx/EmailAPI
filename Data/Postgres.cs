using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext
{
    // ← AGREGAR: Constructor para DI
    public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
    {
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Book> Books { get; set; }

    // ← QUITAR: Ya no necesitas OnConfiguring porque usas DI en Program.cs
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseNpgsql("Host=192.168.1.15;Database=books;Username=example_user;Password=example_password");
}