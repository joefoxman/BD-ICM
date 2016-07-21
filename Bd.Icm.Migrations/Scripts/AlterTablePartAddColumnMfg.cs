using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604131643)]
    public class AlterTablePartAddColumnMfg : Migration
    {
        public override void Up()
        {
            Alter.Table(DbSchema.Tables.Part).AddColumn("Manufacturer").AsString(150).Nullable();
        }

        public override void Down()
        {
            Delete.Column("Manufacturer").FromTable(DbSchema.Tables.Part);
        }
    }
}
