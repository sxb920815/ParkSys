using System.Data.Entity;
using Model;

namespace DAL.MB
{
    public class MbContext:DbContext
    {
         static MbContext()
        {
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<MbContext, MbContextInit>());
        }

        public MbContext()
            : base("name=ParkCNI")
        {

        }


        public DbSet<User> User { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<CardAnnal> CardAnnal { get; set; }
        public DbSet<Wave> Wave { get; set; }
        public DbSet<WaveAnnal> WaveAnnal { get; set; }

        public DbSet<Equipment> Equipment { get; set; }

        public DbSet<AreaAndEqu> AreaAndEqu { get; set; }

        public DbSet<BreakRuleAnnal> BreakRuleAnnal { get; set; }


        public DbSet<Freshen> Freshen { get; set; }
    }
}
