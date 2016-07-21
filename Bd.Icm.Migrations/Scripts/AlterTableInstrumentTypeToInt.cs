using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603151510)]
    public class AlterTableInstrumentTypeToInt : Migration
    {
        public override void Up()
        {
            Alter.Table("Instrument").AlterColumn("Type").AsInt32();
        }

        public override void Down()
        {
            Alter.Table("Instrument").AlterColumn("Type").AsString(150).Nullable();
        }
    }
}
