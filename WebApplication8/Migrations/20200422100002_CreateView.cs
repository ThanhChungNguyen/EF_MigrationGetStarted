using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using WebApplication8.Models;

namespace WebApplication8.Migrations
{
    public partial class CreateView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder )
        {
            //string script =
            //@"
            //CREATE VIEW dbo.UserDTO
            //AS SELECT CourseID, Title
            //FROM [dbo].[Course];";

            string script;
            string str = Directory.GetCurrentDirectory();
            var resourcePath = Path.GetFullPath($"{str}/Migrations/TextFile.sql");
            using (StreamReader file = File.OpenText(resourcePath))
            {
                 script =  file.ReadToEnd();
            }


            var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
            optionsBuilder.UseSqlServer("Server =.;Database=EFStart;Trusted_Connection=True;");

            using (var context = new BloggingContext(optionsBuilder.Options))
            {
                context.Database.ExecuteSqlCommand(script);
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=EFStart;Trusted_Connection=True;");

            using (var context = new BloggingContext(optionsBuilder.Options))
            {
                context.Database.ExecuteSqlCommand("DROP VIEW dbo.UserDTO");
            }
        }
    }
}
