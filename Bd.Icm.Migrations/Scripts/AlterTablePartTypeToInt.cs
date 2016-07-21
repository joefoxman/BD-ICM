using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603151445)]
    public class AlterTablePartTypeToInt : Migration
    {
        public override void Up()
        {
            Alter.Table("Part").AlterColumn("SapPartType").AsInt32();
        }

        public override void Down()
        {
            Alter.Table("Part").AlterColumn("SapPartType").AsString(25).Nullable();
        }
    }
}
