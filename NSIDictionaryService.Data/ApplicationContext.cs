using Microsoft.EntityFrameworkCore;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;

namespace NSIDictionaryService.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            if (Database.IsRelational())
            {
                Database.SetCommandTimeout(20000);
            }

        }


        #region Users
        public DbSet<User> Users => Set<User>();

        public DbSet<Role> Roles => Set<Role>();
        #endregion

        #region Upload
        public DbSet<UploadResult> UploadResults => Set<UploadResult>();

        public DbSet<UploadMethod> UploadMethods => Set<UploadMethod>();

        public DbSet<UploadInfo> Uploads => Set<UploadInfo>();
        #endregion

        #region Dicts
        public DbSet<DictCode> DictCodes => Set<DictCode>();

        public DbSet<DictVersion> Versions => Set<DictVersion>();

        public DbSet<DictProperty> Properties => Set<DictProperty>();   

        public DbSet<Change> Changes => Set<Change>();

        public DbSet<DictDependancy> Dependencies => Set<DictDependancy>();

        public DbSet<V006Dictionary> V006 => Set<V006Dictionary>();

        public DbSet<V012Dictionary> V012 => Set<V012Dictionary>();

        public DbSet<V021Dictionary> V021 => Set<V021Dictionary>();

        public DbSet<V025Dictionary> V025 => Set<V025Dictionary>();
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DictDependancy>()
                .HasOne(d => d.DictCode)
                .WithMany()
                .HasForeignKey(d => d.DictId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DictDependancy>()
                .HasOne(d => d.DependancyCode)
                .WithMany()
                .HasForeignKey(d => d.DependancyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DictCode>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<DictVersion>()
                .Property(x => x.VersionCode)
                .HasPrecision(5, 2);

            modelBuilder.Entity<V025Dictionary>()
                .Property(x => x.Code)
                .HasPrecision(5, 2);

            modelBuilder.Entity<DictVersion>()
                .HasOne(x => x.DictCode)
                .WithMany()
                .HasForeignKey(x => x.DictCodeId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
