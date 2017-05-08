using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities.Entities;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class EFContext : DbContext, IDataContextAsync
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
            
        }

        public DbSet<Book>Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().Property(x => x.Price).HasColumnType("Money");
            modelBuilder.Entity<Book>().Property(x => x.Price).IsRequired();
            modelBuilder.Entity<Book>().Property(x => x.ChategoryId).IsRequired();
            modelBuilder.Entity<Book>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Book>().Property(x => x.Name).HasMaxLength(150);
            modelBuilder.Entity<Book>().Property(x => x.Author).IsRequired();
            modelBuilder.Entity<Book>().Property(x => x.Author).HasMaxLength(150);
            modelBuilder.Entity<Book>().HasOne(x => x.Category).WithMany().HasForeignKey(x => x.ChategoryId);

            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Category>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<Category>().HasMany(x => x.Books).WithOne(x => x.Category);

            modelBuilder.Entity<Order>().Property(x => x.TotalPrice).HasColumnType("Money");
            modelBuilder.Entity<Order>().HasOne(x => x.Book);
            modelBuilder.Entity<Order>().HasOne(x => x.User).WithMany(x => x.Orders).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Order>().HasOne(x => x.Book).WithMany(x => x.Orders).HasForeignKey(x => x.BookId);

        }

        public async Task<int> SaveChangesAsync()
        {
            SyncObjectsStatePreCommit();
            var resp = await base.SaveChangesAsync(CancellationToken.None);
            SyncObjectsStatePostCommit();
            return resp;
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        public override int SaveChanges()
        {
            SyncObjectsStatePreCommit();
            var changes = base.SaveChanges();
            SyncObjectsStatePostCommit();
            return changes;
        }


        private void SyncObjectsStatePreCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
                dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
                ((IObjectState)dbEntityEntry.Entity).ObjectState = StateHelper.ConvertState(dbEntityEntry.State);
        }
    }
}
