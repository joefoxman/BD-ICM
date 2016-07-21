using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603110010)]
    public class CreateTablePartMetadataVersion : Migration
    {
        public override void Up()
        {
            Create.Table("PartMetadataVersion")
                .WithColumn("PartMetadataId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("DbVersion").AsInt32().NotNullable()
                .WithAuditFields()
                .WithRowVersionTimestamp();

            this.CreateUserForeignKeys("PartMetadataVersion");
        }

        public override void Down()
        {
            this.DeleteUserForeignKeys("PartMetadataVersion");
            Delete.Table("PartMetadataDbVersion");
        }
    }
}
