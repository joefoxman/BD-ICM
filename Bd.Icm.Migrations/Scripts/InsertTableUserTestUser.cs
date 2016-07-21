using System;
using FluentMigrator;
using FluentMigrator.Runner.Extensions;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603080911)]
    [Tags("TestSetup")]
    public class InsertTableUserTestUser : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("User").InSchema("dbo").WithIdentityInsert().Row(new
            {
                Id = 1,
                UserName = "testuser@test.com",
                Password = "bd2016",
                FirstName = "Test",
                LastName = "User",
                IsDisabled = false,
                Email = "testuser@test.com",
                CreatedDate = DateTime.Now,
                CreatedBy = 1,
                ModifiedDate = DateTime.Now,
                ModifiedBy = 1
            });
        }

        public override void Down()
        {
            Delete.FromTable("User").InSchema("dbo").Row(new { UserName = "testuser@test.com" });
        }
    }
}
