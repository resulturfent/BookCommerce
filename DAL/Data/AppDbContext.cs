using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
       
           

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne()
                .HasForeignKey(ci => ci.CartId);


           

            modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                 .HasOne(p => p.Author)
                 .WithMany(u => u.BookAuthors)
                 .HasForeignKey(p => p.AuthorId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookAuthor>()
                 .HasOne(p => p.Book)
                 .WithMany(u => u.BookAuthors)
                 .HasForeignKey(p => p.BookId)
                 .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<BookAuthor>()
            //    .HasOne(ba => ba.Book)
            //    .WithMany(b => b.BookAuthors)
            //    .HasForeignKey(ba => ba.BookId)
            //    .OnDelete(DeleteBehavior.Cascade); // Cascade Delete ayarı

            modelBuilder.Entity<Book>()
                .HasOne(b => b.User) // Book tablosunda User ilişkisi
                .WithMany(u => u.Books) // User tablosunda Books koleksiyonu
                .HasForeignKey(b => b.UserId) // Book tablosundaki Foreign Key
                .OnDelete(DeleteBehavior.NoAction); // Silme davranışı
                                                    
                  //modelBuilder.Entity<Book>()
                  //.HasOne(b => b.User)
                  //.WithMany(u => u.Books)
                  //.HasForeignKey(b => b.UserId)
                  //.IsRequired(false); // Foreign Key'i opsiyonel yapar



            modelBuilder.Entity<Book>()
                .Property(x => x.UnitPrice)
                .HasColumnType("decimal(7,2)");

            //modelBuilder.Entity<Order>()
            //    .Property(x => x.OrderAmount)
            //    .HasPrecision(7, 2);

            //modelBuilder.Entity<OrderItem>()
            //    .Property(x => x.Price)
            //    .HasPrecision(7, 2);

            //modelBuilder.Entity<ShoppingCartItem>()
            //    .Property(x => x.Price)
            //    .HasColumnType("decimal(7,2)");

            modelBuilder.Entity<Book>()
                .Property(x => x.UnitPrice)
                .HasPrecision(7, 2);

            modelBuilder.Entity<User>().HasData(

        new User()
        {
            Id = 1,
            IsAdmin = true,
            Email = "admin@gmail.com",
            Name = "MTO",
            Surname = "MTO",
            Password = "123456M+", // Password hashleme gösterilecek unutma, unutturma
            Username = "MTO"

           });
        }

    }
}
