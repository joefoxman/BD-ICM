using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604050843)]
    public class AlterTablePartActionAddColumnInstrumentCommitId : Migration
    {
        public override void Up()
        {
            Alter.Table(DbSchema.Tables.PartAction)
                .AddColumn("InstrumentCommitId").AsInt32().Nullable()
                .ForeignKey("FK_PartAction_InstrumentCommit", DbSchema.Tables.InstrumentCommit, "Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_PartAction_InstrumentCommit").OnTable(DbSchema.Tables.PartAction).InSchema(DbSchema.Schema);
            Delete.Column("InstrumentCommitId").FromTable(DbSchema.Tables.PartAction);
        }
    }
}
