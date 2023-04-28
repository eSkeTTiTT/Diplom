using Diplom.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

        public DbSet<User> Users { get; set; } = null!;

        #region Private Properties

        private IConfiguration _config;

        #endregion

        #region Constructors

        public ApplicationDbContext(IConfiguration config)
        {
            _config = config;
        }

        #endregion

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config.GetConnectionString("SqlServer");

            optionsBuilder.UseSqlServer(connectionString);
        }

        #endregion
    }
}
