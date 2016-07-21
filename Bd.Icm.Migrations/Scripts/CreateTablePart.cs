using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603110007)]
    public class CreateTablePart : Migration
    {
        public override void Up()
        {
            Create.Table("Part")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
                .WithRowVersionIdentity()
                .WithColumn("ParentPartId").AsInt32().Nullable()
                .WithColumn("InstrumentId").AsInt32().Nullable()
                .WithColumn("Name").AsString(150).NotNullable()
                .WithColumn("SerialNumber").AsString(100).Nullable()
                .WithColumn("DrawingNumber").AsString(50).NotNullable()
                .WithColumn("DashNumber").AsInt32().NotNullable()
                .WithColumn("RevisionNumber").AsInt32().NotNullable()
                .WithColumn("SapPartNumber").AsString(50).Nullable()
                .WithColumn("SapPartType").AsString(25).Nullable()
                .WithColumn("LotCode").AsString(50).Nullable()
                .WithColumn("DateCode").AsDateTime().Nullable()
                .WithCommitFields()
                .WithAuditFields()
                .WithVersioningFields();

            this.CreateUserForeignKeys("Part");

            Create.ForeignKey("FK_Part_PartVersion")
                .FromTable("Part").InSchema("dbo").ForeignColumn("Id")
                .ToTable("PartVersion").InSchema("dbo").PrimaryColumn("PartId");

            Create.ForeignKey("FK_Part_Part_Parent")
                .FromTable("Part").InSchema("dbo").ForeignColumn("ParentPartId")
                .ToTable("PartVersion").InSchema("dbo").PrimaryColumn("PartId");

            Create.ForeignKey("FK_Part_InstrumentVersion")
                .FromTable("Part").InSchema("dbo").ForeignColumn("InstrumentId")
                .ToTable("InstrumentVersion").InSchema("dbo").PrimaryColumn("InstrumentId");

        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Part_InstrumentVersion").OnTable("Part");
            Delete.ForeignKey("FK_Part_Part_Parent").OnTable("Part");
            Delete.ForeignKey("FK_Part_PartVersion").OnTable("Part");
            this.DeleteUserForeignKeys("Part");
            Delete.Table("Part");
        }
    }
}
