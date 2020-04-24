using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System.IO;
using WebApplication8.Models;

namespace WebApplication8.Migrations
{
    public partial class CreateView2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string baseDir = Directory.GetCurrentDirectory();
            string sqlScript;
            string[] fileEntries = Directory.GetFiles(Path.GetFullPath($"{baseDir}/SqlScripts"));

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("BloggingDatabase").Value;

            var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
            optionsBuilder.UseSqlServer(connectionString);

            foreach (var fileName in fileEntries)
            {
                using (StreamReader file = File.OpenText(fileName))
                {
                    sqlScript = file.ReadToEnd();
                }
                using (var context = new BloggingContext(optionsBuilder.Options))
                {
                    context.Database.ExecuteSqlCommand(sqlScript);
                }
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.customer.json", optional: false, reloadOnChange: true)
                .Build();
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("BloggingDatabase").Value;

            var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new BloggingContext(optionsBuilder.Options))
            {
                context.Database.ExecuteSqlCommand("DROP VIEW dbo.UserDTO");
            }
        }
    }
}
