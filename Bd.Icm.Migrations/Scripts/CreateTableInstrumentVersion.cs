using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603110004)]
    public class CreateTableInstrumentVersion : Migration
    {
        public override void Up()
        {
            Create.Table("InstrumentVersion")
                .WithColumn("InstrumentId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("DbVersion").AsInt32().NotNullable()
                .WithAuditFields()
                .WithRowVersionTimestamp();

            this.CreateUserForeignKeys("InstrumentVersion");
        }

        public override void Down()
        {
            this.DeleteUserForeignKeys("InstrumentVersion");
            Delete.Table("InstrumentDbVersion");
        }
    }
}
