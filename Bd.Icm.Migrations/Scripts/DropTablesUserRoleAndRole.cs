using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603301233)]
    public class DropTablesUserRoleAndRole : Migration
    {
        public override void Up()
        {
            Delete.Table(DbSchema.Tables.UserRole).InSchema(DbSchema.Schema);
            Delete.Table(DbSchema.Tables.Role).InSchema(DbSchema.Schema);
        }

        public override void Down()
        {

            Create.Table("Role")
                .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("Description").AsString(50).NotNullable()
                .WithAuditFields()
                .WithRowVersionTimestamp();

            this.CreateUserForeignKeys("Role");

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
    }
}
