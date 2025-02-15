﻿using Microsoft.EntityFrameworkCore;

namespace Shopping.List.App.Blazor.Database.ShoppingList;

public class ShoppingListCtx : DbContext
{
    [Obsolete("Test only", true)]
    public ShoppingListCtx()
    {
    }

    public ShoppingListCtx(DbContextOptions<ShoppingListCtx> options) : base(options)
    {
    }

    public virtual DbSet<ShoppingUser> Users { get; set; }
    public virtual DbSet<ItemList> Lists { get; set; }
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<ItemPicture> Pictures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingUser>(entity =>
            {
                entity.HasKey(su => su.Id);

                entity.HasMany(su => su.Lists)
                    .WithOne()
                    .HasForeignKey(il => il.UserId);

                entity.HasMany(su => su.Pictures)
                    .WithOne()
                    .HasForeignKey(p => p.UserId);
            }
        );

        modelBuilder.Entity<ItemList>(entity =>
            {
                entity.HasKey(il => il.Id);

                entity.Property(il => il.UserId)
                    .HasColumnName("UserId");

                entity.Property(il => il.CreationDate)
                    .HasColumnName("CreationDate");

                entity.Property(il => il.DueTo)
                    .HasField("_dueTo");

                entity.Property(il => il.IsRealized)
                    .HasField("_isRealized");
            }
        );

        modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.HasOne<ShoppingUser>()
                    .WithMany()
                    .HasForeignKey(i => i.UserId);

                entity.Property(i => i.ItemListId)
                    .HasColumnName("ItemListId");

                entity.Property(i => i.CreationDate)
                    .HasColumnName("CreationDate");

                entity.Property(i => i.Quantity)
                    .HasField("_quantity");

                entity.Property(i => i.IsBought)
                    .HasField("_isBought");
            }
        );

        modelBuilder.Entity<ItemPicture>(entity =>
            {
                entity.HasKey(ip => ip.Id);

                entity.Property(ip => ip.UserId)
                    .HasColumnName("UserId");
            }
        );
    }
}