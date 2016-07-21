using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604131600)]
    public class AlterTableInstrumentCommitAddColumnRevision : Migration
    {
        public override void Up()
        {
            Alter.Table(DbSchema.Tables.InstrumentCommit).AddColumn("Revision").AsInt32().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            Delete.Column("Revision").FromTable(DbSchema.Tables.InstrumentCommit);
        }
    }
}
