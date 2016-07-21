using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603241043)]
    public class AlterTablePartCommittedDateToCommitId : Migration
    {
        public override void Up()
        {
            Delete.Column("CommittedDate").FromTable(DbSchema.Tables.Part);
            Alter.Table(DbSchema.Tables.Part)
                .AddColumn("InstrumentCommitId")
                .AsInt32()
                .Nullable()
                .ForeignKey("FK_Part_InstrumentCommit", DbSchema.Tables.InstrumentCommit, "Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Part_InstrumentCommit").OnTable(DbSchema.Tables.Part);
            Delete.Column("InstrumentCommitId").FromTable(DbSchema.Tables.Part);
            Alter.Table("Part").AddColumn("CommittedDate").AsDateTime().Nullable();
        }
    }
}
