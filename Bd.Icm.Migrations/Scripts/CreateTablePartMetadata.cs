using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603110011)]
    public class CreateTablePartMetadata : Migration
    {
        public override void Up()
        {
               Create.Table("PartMetadata")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
                .WithRowVersionIdentity()
                .WithColumn("PartId").AsInt32().NotNullable()
                .WithColumn("MetaKey").AsString(50).NotNullable()
                .WithColumn("MetaValue").AsString(50).NotNullable()
                .WithAuditFields()
                .WithVersioningFields();

            this.CreateUserForeignKeys("PartMetadata");

            Create.ForeignKey("FK_PartMetadata_PartMetadataVersion")
                .FromTable("PartMetadata").InSchema("dbo").ForeignColumn("Id")
                .ToTable("PartMetadataVersion").InSchema("dbo").PrimaryColumn("PartMetadataId");

            Create.ForeignKey("FK_PartMetadata_PartVersion")
                .FromTable("PartMetadata").InSchema("dbo").ForeignColumn("PartId")
                .ToTable("PartVersion").InSchema("dbo").PrimaryColumn("PartId");

        }

        public override void Down()
        {
            Delete.ForeignKey("FK_PartMetadata_PartVersion").OnTable("PartMetadata");
            Delete.ForeignKey("FK_PartMetadata_PartMetadataVersion").OnTable("PartMetadata");
            this.DeleteUserForeignKeys("PartMetadata");
            Delete.Table("PartMetadata");
        }
    }
}
