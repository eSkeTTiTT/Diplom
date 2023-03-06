using Diplom.DOMAIN;
using Microsoft.EntityFrameworkCore;

namespace Diplom.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Audio> Audios { get; set; } = null!;

        public DbSet<Image> Images { get; set; } = null!;

        public DbSet<KindOfActivity> KindOfActivities { get; set; } = null!;

        public DbSet<Location> Locations { get; set; } = null!;

        public DbSet<Person> Persons { get; set; } = null!;

        public DbSet<Text> Texts { get; set; } = null!;

        public DbSet<Video> Videos { get; set; } = null!;

        #region Constructors

        public ApplicationDbContext()
        {

        }

        #endregion
    }
}
