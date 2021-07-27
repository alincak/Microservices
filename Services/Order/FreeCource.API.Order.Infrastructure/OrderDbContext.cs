﻿using Microsoft.EntityFrameworkCore;

namespace FreeCource.API.Order.Infrastructure
{
  public class OrderDbContext : DbContext
  {
    public const string DEFAULT_SCHEMA = "ordering";

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {  
    }

    public DbSet<Domain.OrderAggregate.Order> Orders { get; set; }
    public DbSet<Domain.OrderAggregate.OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Domain.OrderAggregate.Order>().ToTable("Orders", DEFAULT_SCHEMA);
      modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().ToTable("Orders", DEFAULT_SCHEMA);

      modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().Property(x => x.Price).HasColumnType("decimal(9,2)");

      modelBuilder.Entity<Domain.OrderAggregate.Order>().OwnsOne(o => o.Address).WithOwner();

      base.OnModelCreating(modelBuilder);
    }

  }
}
