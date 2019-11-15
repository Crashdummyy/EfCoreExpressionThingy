using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
                                      {
                                          e.HasOne(x => x.Location)
                                           .WithMany()
                                           .HasForeignKey(x => x.LocationId)
                                           .HasConstraintName("FK_User_Location");
                                      });
        }
    }
}