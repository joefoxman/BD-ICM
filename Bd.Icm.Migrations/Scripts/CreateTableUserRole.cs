using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603110003)]
    public class CreateTableUserRole : Migration
    {
        public override void Up()
        {
            Create.Table("UserRole")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable()
                .WithAuditFields()
                .WithRowVersionTimestamp();

            this.CreateUserForeignKeys("UserRole");

            Create.ForeignKey("FK_UserRole_Role").FromTable("UserRole").InSchema("dbo").ForeignColumn("RoleId")
                .ToTable("Role").InSchema("dbo").PrimaryColumn("Id");
            Create.ForeignKey("FK_UserRole_User").FromTable("UserRole").InSchema("dbo").ForeignColumn("UserId")
                .ToTable("User").InSchema("dbo").PrimaryColumn("Id");
        }

        public override void Down()
        {
            this.DeleteUserForeignKeys("UserRole");
            Delete.ForeignKey("FK_UserRole_User").OnTable("UserRole").InSchema("dbo");
            Delete.ForeignKey("FK_UserRole_Role").OnTable("UserRole").InSchema("dbo");
            Delete.Table("UserRole");
        }
    }
}
