using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603171303)]
    public class FixSerialNumberUniqueConstraint : Migration
    {
        public override void Up()
        {
            Delete.Index("IX_Part_SerialNumber").OnTable("Part").InSchema("dbo");
            Create.Index("IX_Part_SerialNumber").OnTable("Part").InSchema("dbo")
                .OnColumn("SerialNumber").Ascending()
                .OnColumn("RowVersion").Ascending()
                .WithOptions().Unique();
        }

        public override void Down()
        {
            Delete.Index("IX_Part_SerialNumber").OnTable("Part").InSchema("dbo");
            Create.Index("IX_Part_SerialNumber").OnTable("Part").InSchema("dbo").OnColumn("SerialNumber").Ascending().WithOptions().Unique();
        }
    }
}
