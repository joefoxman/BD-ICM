using System;
using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604211706)]
    [Tags("Setup")]
    public class UpdateAdminUserWithHashedPassword : Migration
    {
        public override void Up()
        {
            var hash = User.HashPassword("1");
            Update.Table(DbSchema.Tables.User).InSchema(DbSchema.Schema).Set(new { Password = hash }).Where(new { UserName = "admin" });
        }

        public override void Down()
        {
        }
    }
}
