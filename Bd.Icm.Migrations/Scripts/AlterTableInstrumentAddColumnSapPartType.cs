using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603181252)]
    public class AlterTableInstrumentAddColumnSapPartType : Migration
    {
        public override void Up()
        {
            Alter.Table("Instrument").AddColumn("SapPartType").AsInt32().Nullable();
            Alter.Column("Type").OnTable("Instrument").InSchema("dbo").AsString(200).NotNullable();
        }

        public override void Down()
        {
            Alter.Column("Type").OnTable("Instrument").InSchema("dbo").AsInt32().NotNullable();
            Delete.Column("SapPartType").FromTable("Instrument");
        }
    }
}
