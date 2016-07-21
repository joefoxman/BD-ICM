using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603291131)]
    public class AlterTablePartAddColumnMfgPartNumber : Migration
    {
        public override void Up()
        {
            Alter.Table(DbSchema.Tables.Part).AddColumn("MfgPartNumber").AsString(50).Nullable();
        }

        public override void Down()
        {
            Delete.Column("MfgPartNumber").FromTable(DbSchema.Tables.Part);
        }
    }
}
