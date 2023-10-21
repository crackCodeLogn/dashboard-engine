using engine.Models;
using Microsoft.EntityFrameworkCore;

namespace engine.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Mode> Modes { get; set; }
    public DbSet<Subject> Subjects { get; set; }
}