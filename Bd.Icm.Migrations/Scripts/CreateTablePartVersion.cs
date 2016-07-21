using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603110006)]
    public class CreateTablePartVersion : Migration
    {
        public override void Up()
        {
            Create.Table("PartVersion")
                .WithColumn("PartId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("DbVersion").AsInt32().NotNullable()
                .WithAuditFields()
                .WithRowVersionTimestamp();

            this.CreateUserForeignKeys("PartVersion");
        }

        public override void Down()
        {
            this.DeleteUserForeignKeys("PartVersion");
            Delete.Table("PartDbVersion");
        }
    }
}
