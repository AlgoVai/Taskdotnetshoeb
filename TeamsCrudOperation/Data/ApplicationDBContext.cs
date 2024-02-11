using Microsoft.EntityFrameworkCore;
using TeamsCrudOperation.Models;

namespace TeamsCrudOperation.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {


        }
     public DbSet<TeamInfo> TeamInfos { get; set; }



    }
}
