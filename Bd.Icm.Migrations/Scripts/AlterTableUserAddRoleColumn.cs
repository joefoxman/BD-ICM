using System;
using Bd.Icm.Core;
using FluentMigrator;
using FluentMigrator.Runner.Extensions;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603301236)]
    public class AlterTableUserAddRoleColumn : Migration
    {
        public override void Up()
        {
            Alter.Table(DbSchema.Tables.User).AddColumn("Role").AsInt32().NotNullable().WithDefaultValue(0);

            Insert.IntoTable(DbSchema.Tables.User).Row(new
            {
                Id = 1,
                UserName = "admin",
                Password = "r1Vw9aGBC3r3jK9LxwpmDw31HkK6+R1N5bIyjeDoPfw=",
                FirstName = "System",
                LastName = "Administrator",
                IsDisabled = false,
                Email = "admin@mindovermachines.com",
                Role = 3,
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                ModifiedBy = 1,
                ModifiedDate = DateTime.Now
            }).WithIdentityInsert();

            this.CreateUserForeignKeys("User");

            Insert.IntoTable(DbSchema.Tables.DbVersion)
                .InSchema(DbSchema.Schema)
                .Row(new {DbVersion = 0, ModifiedDate = DateTime.Now, ModifiedBy = 1});
        }

        public override void Down()
        {
            this.DeleteUserForeignKeys("User");
            Delete.Column("Role").FromTable(DbSchema.Tables.User);
        }
    }
}
