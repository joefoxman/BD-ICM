using System;
using FluentMigrator;
using FluentMigrator.Runner.Extensions;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604201306)]
    [Tags("TestSetup")]
    public class SetRoleForTestUser : Migration
    {
        public override void Up()
        {
            Insert.IntoTable(DbSchema.Tables.UserRole)
                .Row(
                    new
                    {
                        Id = 1,
                        RoleId = 3,
                        UserId = 1,
                        CreatedBy = 1,
                        ModifiedBy = 1,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    }).WithIdentityInsert();
        }

        public override void Down()
        {
            Delete.FromTable(DbSchema.Tables.UserRole).InSchema(DbSchema.Schema).AllRows();
        }
    }
}
