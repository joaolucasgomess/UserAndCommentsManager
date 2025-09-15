using Microsoft.EntityFrameworkCore;
using UserAndCommentsManager.Models;

namespace UserAndCommentsManager.Data;

public class MinimalContextDb : DbContext
{
    public MinimalContextDb(DbContextOptions<MinimalContextDb> options) :  base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
