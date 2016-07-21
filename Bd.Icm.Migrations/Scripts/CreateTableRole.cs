using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603110002)]
    public class CreateTableRole : Migration
    {
        public override void Up()
        {
            Create.Table("Role")
                .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("Description").AsString(50).NotNullable()
                .WithAuditFields()
                .WithRowVersionTimestamp();

            this.CreateUserForeignKeys("Role");
        }

        public override void Down()
        {
            this.DeleteUserForeignKeys("Role");
            Delete.Table("Role");
        }
    }
}
