using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604141242)]
    public class AlterTablePartMetadataAddInstrumentCommitIdColumn : Migration
    {
        public override void Up()
        {
            Alter.Table(DbSchema.Tables.PartMetadata)
                .AddColumn("InstrumentCommitId").AsInt32().Nullable()
                .ForeignKey("FK_PartMetadata_InstrumentCommit", DbSchema.Tables.InstrumentCommit, "Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_PartMetadata_InstrumentCommit").OnTable(DbSchema.Tables.PartMetadata).InSchema(DbSchema.Schema);
            Delete.Column("InstrumentCommitId").FromTable(DbSchema.Tables.PartMetadata);
        }
    }
}
