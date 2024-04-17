using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUI.Models;

namespace WebUI.Data;

public class AppDbConext : IdentityDbContext<User>
{
    
    public AppDbConext(DbContextOptions<AppDbConext> options) : base (options)
    {
    }


    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityRole>().ToTable("Roles");
    }
}
