using System.Data.Entity;

namespace _2C2P_CodeChallenge.Models
{
    public class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
    }
}
