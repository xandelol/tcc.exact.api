using System.Linq;
using exact.api.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace exact.api.Data
{
    public class ExactContext: DbContext
    {
        public ExactContext(DbContextOptions<ExactContext> options)
            : base(options)
        {

        }

        public DbSet<ActionEntity> Actions { get; set; }
        public DbSet<GroupActionEntity> GroupActions { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<SettingEntity> Settings { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

        }
    }
}