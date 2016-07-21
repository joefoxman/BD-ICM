using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603151358)]
    public class SerialNumberUniqueConstraint : Migration
    {
        public override void Up()
        {
            Create.Index("IX_Part_SerialNumber").OnTable("Part").InSchema("dbo").OnColumn("SerialNumber").Ascending().WithOptions().Unique();
        }

        public override void Down()
        {
            Delete.Index("IX_Part_SerialNumber").OnTable("Part").InSchema("dbo");
        }
    }
}
