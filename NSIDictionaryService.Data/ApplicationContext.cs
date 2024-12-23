using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Data.Models.Users;

namespace NSIDictionaryService.Data
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            if (Database.IsRelational())
            {
                Database.SetCommandTimeout(20000);
            }

        }

        //#region Users
        //public DbSet<User> Users => Set<User>();

        //public DbSet<IdentityRole> Roles => Set<IdentityRole>();

        //#endregion

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

        public DbSet<DictDependency> Dependencies => Set<DictDependency>();

        public DbSet<V006Dictionary> V006 => Set<V006Dictionary>();

        public DbSet<V012Dictionary> V012 => Set<V012Dictionary>();

        public DbSet<V021Dictionary> V021 => Set<V021Dictionary>();

        public DbSet<V025Dictionary> V025 => Set<V025Dictionary>();
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UploadMethod>()
                .HasData(
                new UploadMethod { Id = 1, Name = "Ручная загрузка"},
                new UploadMethod { Id = 2, Name = "Загрузка из API"},
                new UploadMethod { Id = 3, Name = "Загрузка из XML файла"}
                );

            modelBuilder.Entity<UploadResult>()
                .HasData(
                new UploadResult { Id = 1, Name = "Успешно" },
                new UploadResult { Id = 2, Name = "Ошибка" }
                );

            modelBuilder.Entity<DictProperty>()
                .HasData(
                new DictProperty { Id = 1, DictCodeId = 1, PropertyName = "Code", PropertyCode = "IDUMP"},
                new DictProperty { Id = 2, DictCodeId = 1, PropertyName = "Name", PropertyCode = "UMPNAME" },
                new DictProperty { Id = 3, DictCodeId = 1, PropertyName = "BeginDate", PropertyCode = "DATEBEG" },
                new DictProperty { Id = 4, DictCodeId = 1, PropertyName = "EndDate", PropertyCode = "DATEEND" }
                );

            modelBuilder.Entity<DictCode>()
                .HasData(
                new DictCode() { Id = 1, Name = "V006"},
                new DictCode() { Id = 2, Name = "V012"},
                new DictCode() { Id = 3, Name = "V021"},
                new DictCode() { Id = 4, Name = "V025"}
                );

            modelBuilder.Entity<DictCode>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<DictDependency>()
                .HasOne(d => d.DictCode)
                .WithMany()
                .HasForeignKey(d => d.DictId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DictDependency>()
                .HasOne(d => d.DependencyCode)
                .WithMany()
                .HasForeignKey(d => d.DependencyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DictDependency>()
                .HasData(
                new DictDependency { Id = 1, DictId = 2, DependencyId = 1}
                );

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
