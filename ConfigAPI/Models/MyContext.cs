using ConfigAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigAPI.Model
{
    public class MyContext:DbContext
    {
        public DbSet<Item> Items { get; set; }

        public DbSet<Component> Components { get; set; }

        public DbSet<Configuration> Configurations { get; set; }

        public DbSet<ItemsComponents> ItemsComponents { get; set; }

        public DbSet<ConfigurationsItems> ConfigurationsItems { get; set; }


        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {


        }
    }
}
