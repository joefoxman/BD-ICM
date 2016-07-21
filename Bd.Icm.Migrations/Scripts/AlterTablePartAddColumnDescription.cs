using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603151349)]
    public class AlterTablePartAddColumnDescription : Migration
    {
        public override void Up()
        {
            Alter.Table("Part").AddColumn("Description").AsString(200).Nullable();
        }

        public override void Down()
        {
            Delete.Column("Description").FromTable("Part");
        }
    }
}
