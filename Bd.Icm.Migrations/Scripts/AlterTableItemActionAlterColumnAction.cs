using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603220917)]
    public class AlterTablePartActionAlterColumnAction : Migration
    {
        public override void Up()
        {
            Alter.Table(DbSchema.Tables.PartAction).AlterColumn("Action").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Alter.Table(DbSchema.Tables.PartAction).AlterColumn("Action").AsString(50).NotNullable();
        }
    }
}
