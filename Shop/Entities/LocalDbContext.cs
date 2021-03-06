using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class LocalDbContext : DbContext
    {
        private readonly IPasswordHasher<User> passwordHasher;

        public LocalDbContext(DbContextOptions<LocalDbContext> options, IPasswordHasher<User> passwordHasher) : base(options)
        {
            this.passwordHasher = passwordHasher;
        }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bucket> Buckets { get; set; }
        public DbSet<BucketItem> BucketItems { get; set; }
        public DbSet<StateOrder> StateOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemOrder> ItemOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ShopSeeder(modelBuilder, passwordHasher).SeedDatabase();

            modelBuilder.Entity<User>(eb =>
           {
               eb.HasMany(u => u.Item)
               .WithOne(i => i.User)
               .HasForeignKey(i => i.UserId);
           });


            modelBuilder.Entity<UserRole>(eb =>
            {
                
            });
            modelBuilder.Entity<Order>(eb =>
            {
                eb.HasOne( o => o.User)
                .WithMany( u => u.Orders)
                .HasForeignKey(o => o.UserId);

                eb.HasMany(o => o.Items)
                .WithMany(i => i.Orders)
                .UsingEntity<ItemOrder>(
                    io => io.HasOne(i => i.Item)
                    .WithMany()
                    .HasForeignKey(i => i.ItemId),
                    io => io.HasOne(i => i.Order)
                    .WithMany()
                    .HasForeignKey(i => i.OrderId),

                    i =>
                    {
                        i.HasKey(k => k.Id);
                    }

                    ); 
            });
            
            modelBuilder.Entity<StateOrder>(eb =>
            {
               eb.HasMany(os => os.Orders)
               .WithOne(o => o.StateOrder)
               .HasForeignKey(o => o.StateOrderId);
            });
    
            modelBuilder.Entity<Bucket>(eb =>
            {
                eb.HasOne(b => b.User).
                WithOne(u => u.Bucket).
                HasForeignKey<Bucket>(b => b.UserId);

            eb.HasMany(b => b.Items)
            .WithMany(i => i.Buckets)
            .UsingEntity<BucketItem>(
                bi => bi.HasOne(i => i.Item)
                .WithMany()
                .HasForeignKey(i => i.ItemId),
                bi => bi.HasOne(i => i.Bucket)
                .WithMany()
                .HasForeignKey(i => i.BucketId),
                i =>
                {
                    i.HasKey(k => k.Id);

                }

                );
            });
            modelBuilder.Entity<Category>(eb =>
            {
               eb.HasMany(c => c.Item)
               .WithOne(i => i.Category)
               .HasForeignKey(c => c.CategoryId);

            });

        }
    }
}
