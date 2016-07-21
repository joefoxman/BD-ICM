using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603110008)]
    public class CreateTablePartActionVersion : Migration
    {
        public override void Up()
        {
            Create.Table("PartActionVersion")
                .WithColumn("PartActionId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("DbVersion").AsInt32().NotNullable()
                .WithAuditFields()
                .WithRowVersionTimestamp();

            this.CreateUserForeignKeys("PartActionVersion");
        }

        public override void Down()
        {
            this.DeleteUserForeignKeys("PartActionVersion");
            Delete.Table("PartActionDbVersion");
        }
    }
}
