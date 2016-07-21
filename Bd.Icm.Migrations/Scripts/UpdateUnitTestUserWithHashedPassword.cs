using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604131610)]
    [Tags("TestSetup")]
    public class UpdateUnitTestUserWithHashedPassword : Migration
    {
        public override void Up()
        {
            Update.Table(DbSchema.Tables.User).InSchema(DbSchema.Schema).Set(new { Password = "sPZq3INkFYZlaGaBP9ndC467Y3lgdWYbpF0aqAieHUQ=" }).Where(new { UserName = "testuser@test.com" });
            Alter.Table("Instrument").AlterColumn("Type").AsInt32();
        }

        public override void Down()
        {
        }
    }
}
