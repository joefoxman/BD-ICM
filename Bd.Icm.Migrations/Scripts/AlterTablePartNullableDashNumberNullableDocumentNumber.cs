using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604251442)]
    public class AlterTablePartNullableDashNumberNullableDocumentNumber : Migration
    {
        public override void Up()
        {
            Alter.Table("Part").AlterColumn("DashNumber").AsInt32().Nullable();
            Alter.Table("Part").AlterColumn("DocumentNumber").AsString(50).Nullable();
        }

        public override void Down()
        {
            Execute.Sql("UPDATE Part SET DashNumber=0 WHERE DashNumber IS NULL");
            Execute.Sql("UPDATE Part SET DocumentNumber='???' WHERE DocumentNumber IS NULL");
            Alter.Table("Part").AlterColumn("DashNumber").AsInt32().NotNullable();
            Alter.Table("Part").AlterColumn("DocumentNumber").AsString(50).NotNullable();
        }
    }
}
