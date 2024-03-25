using CRUD_Thunders.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CRUD_Thunders.Infra.Infrastructure.CRUDContext
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Activity> Activity { get; set; }
    }
}
