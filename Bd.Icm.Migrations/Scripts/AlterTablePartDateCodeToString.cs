using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603151357)]
    public class AlterTablePartDateCodeToString : Migration
    {
        public override void Up()
        {
            Alter.Table("Part").AlterColumn("DateCode").AsString(20).Nullable();
        }

        public override void Down()
        {
            Alter.Table("Part").AlterColumn("DateCode").AsDateTime().Nullable();
        }
    }
}
