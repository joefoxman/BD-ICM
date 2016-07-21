using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603281332)]
    public class AlterTableInstrumentAddRevision : Migration
    {
        public override void Up()
        {
            Alter.Table(DbSchema.Tables.Instrument).AddColumn("Revision").AsInt32().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            Delete.Column("Revision").FromTable(DbSchema.Tables.Instrument);
        }
    }
}
