using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604251518)]
    public class AlterTableInstrumentCommitNotesMaxTo4000 : Migration
    {
        public override void Up()
        {
            Alter.Table("InstrumentCommit").AlterColumn("Notes").AsString(4000).Nullable();
        }

        public override void Down()
        {
            Alter.Table("InstrumentCommit").AlterColumn("Notes").AsString(255).Nullable();
        }
    }
}
