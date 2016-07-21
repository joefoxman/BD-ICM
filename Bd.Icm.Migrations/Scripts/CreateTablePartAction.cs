using FluentMigrator;
using FluentMigrator.Expressions;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603110009)]
    public class CreateTablePartAction : Migration
    {
        public override void Up()
        {
            Create.Table("PartAction")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
                .WithRowVersionIdentity()
                .WithColumn("PartId").AsInt32().NotNullable()
                .WithColumn("Action").AsString(50).NotNullable()
                .WithColumn("Description").AsString(300).Nullable()
                .WithColumn("ActionDate").AsDateTime().NotNullable()
                .WithAuditFields()
                .WithVersioningFields();

            this.CreateUserForeignKeys("PartAction");

            Create.ForeignKey("FK_PartAction_PartActionVersion")
                .FromTable("PartAction").InSchema("dbo").ForeignColumn("Id")
                .ToTable("PartActionVersion").InSchema("dbo").PrimaryColumn("PartActionId");

            Create.ForeignKey("FK_PartAction_PartVersion")
                .FromTable("PartAction").InSchema("dbo").ForeignColumn("PartId")
                .ToTable("PartVersion").InSchema("dbo").PrimaryColumn("PartId");

        }

        public override void Down()
        {
            Delete.ForeignKey("FK_PartAction_PartVersion").OnTable("PartAction");
            Delete.ForeignKey("FK_PartAction_PartActionVersion").OnTable("PartAction");
            this.DeleteUserForeignKeys("PartAction");
            Delete.Table("PartAction");
        }
    }
}
