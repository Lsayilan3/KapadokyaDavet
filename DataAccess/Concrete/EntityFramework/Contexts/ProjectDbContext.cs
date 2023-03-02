using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Because this context is followed by migration for more than one provider
    /// works on PostGreSql db by default. If you want to pass sql
    /// When adding AddDbContext, use MsDbContext derived from it.
    /// </summary>
    public class ProjectDbContext : DbContext
    {
        /// <summary>
        /// in constructor we get IConfiguration, parallel to more than one db
        /// we can create migration.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration)
                                                                                : base(options)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Let's also implement the general version.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        protected ProjectDbContext(DbContextOptions options, IConfiguration configuration)
                                                                        : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupClaim> GroupClaims { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<MobileLogin> MobileLogins { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Translate> Translates { get; set; }
        public DbSet<Cikolata> Cikolatas { get; set; }
        public DbSet<Ensatan> Ensatans { get; set; }
        public DbSet<GalleryTwo> GalleryTwoes { get; set; }
        public DbSet<Hediyelik> Hediyeliks { get; set; }
        public DbSet<Muzik> Muziks { get; set; }
        public DbSet<OrAcilis> OrAcilises { get; set; }
        public DbSet<OrAnimasyone> OrAnimasyones { get; set; }
        public DbSet<OrBaby> OrBabies { get; set; }
        public DbSet<OrCatering> OrCaterings { get; set; }
        public DbSet<OrCikolata> OrCikolatas { get; set; }
        public DbSet<OrCoffe> OrCofves { get; set; }
        public DbSet<OrDugun> OrDuguns { get; set; }
        public DbSet<OrEkipman> OrEkipmans { get; set; }
        public DbSet<Organizasyon> Organizasyons { get; set; }
        public DbSet<OrKinaa> OrKinaas { get; set; }
        public DbSet<OrKokteyl> OrKokteyls { get; set; }
        public DbSet<OrNisan> OrNisans { get; set; }
        public DbSet<OrPartiEglence> OrPartiEglences { get; set; }
        public DbSet<OrPartiStore> OrPartiStores { get; set; }
        public DbSet<OrPersonelTemini> OrPersonelTeminis { get; set; }
        public DbSet<OrPiknik> OrPikniks { get; set; }
        public DbSet<OrSirketEglence> OrSirketEglences { get; set; }
        public DbSet<OrSokakLezzeti> OrSokakLezzetis { get; set; }
        public DbSet<OrSunnet> OrSunnets { get; set; }
        public DbSet<OrTrioEkibi> OrTrioEkibis { get; set; }
        public DbSet<Parti> Partis { get; set; }
        public DbSet<SliderTwo> SliderTwoes { get; set; }
        public DbSet<Spot> Spots { get; set; }
        public DbSet<SpotCategoryy> SpotCategoryies { get; set; }
        public DbSet<Yiyecek> Yiyeceks { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Ensonurun> Ensonuruns { get; set; }
        public DbSet<Lazer> Lazers { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DArchPgContext")).EnableSensitiveDataLogging());

            }
        }

    }
}
