using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201605171717)]
    [Tags("TestSetup")]
    public class UpdateUnitTestUserWithHashedPassword2 : Migration
    {
        public override void Up()
        {
            Update.Table(DbSchema.Tables.User).InSchema(DbSchema.Schema).Set(new { Password = "c4ca4238a0b923820dcc509a6f75849b" }).Where(new { UserName = "testuser@test.com" });
        }

        public override void Down()
        {
        }
    }
}
