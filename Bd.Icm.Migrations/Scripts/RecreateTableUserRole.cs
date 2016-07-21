using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604201300)]
    public class RecreateTableUserRole : Migration
    {
        public override void Up()
        {
            Create.Table(DbSchema.Tables.UserRole)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable()
                .WithAuditFields()
                .WithRowVersionTimestamp();

            this.CreateUserForeignKeys(DbSchema.Tables.UserRole);

            Create.ForeignKey("FK_UserRole_User").FromTable(DbSchema.Tables.UserRole).InSchema(DbSchema.Schema).ForeignColumn("UserId")
                .ToTable(DbSchema.Tables.User).InSchema(DbSchema.Schema).PrimaryColumn("Id");
        }

        public override void Down()
        {
            this.DeleteUserForeignKeys(DbSchema.Tables.UserRole);
            Delete.ForeignKey("FK_UserRole_User").OnTable(DbSchema.Tables.UserRole).InSchema(DbSchema.Schema);
            Delete.Table(DbSchema.Tables.UserRole);
        }
    }
}
