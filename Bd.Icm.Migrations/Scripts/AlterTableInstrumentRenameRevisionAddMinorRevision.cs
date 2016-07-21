using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604050736)]
    public class AlterTableInstrumentRenameRevisionAddMinorRevision : Migration
    {
        public override void Up()
        {
            Rename.Column("Revision").OnTable(DbSchema.Tables.Instrument).To("MajorRevision");
            Alter.Table(DbSchema.Tables.Instrument).AddColumn("MinorRevision").AsInt32().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            Rename.Column("MajorRevision").OnTable(DbSchema.Tables.Instrument).To("Revision");
            Delete.Column("MinorRevision").FromTable(DbSchema.Tables.Instrument);
        }
    }
}
