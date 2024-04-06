﻿using GameStore.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GameStore.Server.Data
{
    public class GameStorecontext : DbContext
    {
        public GameStorecontext(DbContextOptions<GameStorecontext> options) : base(options) { }
        public DbSet<Game> Games => Set<Game>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
