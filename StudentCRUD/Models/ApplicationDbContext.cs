using Microsoft.EntityFrameworkCore;

namespace StudentCRUD.Models
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Students> Students { get; set; }
    }
}
