using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604140828)]
    public class AlterTableInstrumentCommitAddColumnEffectiveTo : Migration
    {
        public override void Up()
        {
            Alter.Table(DbSchema.Tables.InstrumentCommit).AddColumn("EffectiveTo").AsInt32().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            Delete.Column("EffectiveTo").FromTable(DbSchema.Tables.InstrumentCommit);
        }
    }
}
