using Folders.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Folders.DB
{
    public class FoldersContext : DbContext
    {
        public FoldersContext(DbContextOptions<FoldersContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var creatingDigitalImages = new Folder
            {
                Id = 1,
                Name = "Creating Digital Images"
            };
            var resources = new Folder
            {
                Id = 2,
                Name = "Resources",
                ParentId = 1
            };
            var primarySources = new Folder
            {
                Id = 3,
                Name = "Primary Sources",
                ParentId = 2
            };
            var secondarySources = new Folder
            {
                Id = 4,
                Name = "Secondary Sources",
                ParentId = 2
            };
            var evidence = new Folder
            {
                Id = 5,
                Name = "Evidence",
                ParentId = 1
            };
            var graphicProducts = new Folder
            {
                Id = 6,
                Name = "Graphic Products",
                ParentId = 1
            };
            var process = new Folder
            {
                Id = 7,
                Name = "Process",
                ParentId = 6
            };
            var finalProduct = new Folder
            {
                Id = 8,
                Name = "Final Product",
                ParentId = 6
            };

            modelBuilder.Entity<Folder>().HasData(creatingDigitalImages, resources, primarySources, secondarySources, evidence, graphicProducts, process, finalProduct);
            modelBuilder.Entity<RootFolder>().HasData(new RootFolder { Id = 1, RootId = 1 });
        }

        public DbSet<RootFolder> Roots { get; set; }

        public DbSet<Folder> Folders { get; set; }
    }
}
