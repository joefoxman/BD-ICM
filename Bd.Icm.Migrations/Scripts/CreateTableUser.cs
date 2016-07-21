using System;
using FluentMigrator;
using FluentMigrator.Runner.Extensions;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603110001)]
    public class CreateTableUser : Migration
    {
        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("UserName").AsString(50).NotNullable()
                .WithColumn("Password").AsString(50).NotNullable()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(50).NotNullable()
                .WithColumn("Email").AsString(50).NotNullable()
                .WithColumn("IsDisabled").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithAuditFields()
                .WithRowVersionTimestamp();
        }

        public override void Down()
        {
            Delete.Table("User");
        }
    }
}
