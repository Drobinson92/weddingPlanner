using Microsoft.EntityFrameworkCore;
namespace weddingplanner.Models{
    public class PlannerContext: DbContext{
        public PlannerContext(DbContextOptions<PlannerContext> options) : base(options){}
        public DbSet<Wedding> Weddings {get; set;}
        public DbSet<UsersWeddings> UsersWeddings {get; set;}
        public DbSet<User> Users {get; set;}
    }
}