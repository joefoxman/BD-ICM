using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603231100)]
    public class CreateSprocGetNextDbVersion : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE PROCEDURE [dbo].[spGetNextDbVersion]
					@ModifiedBy int
                AS
	SET NOCOUNT ON
	DECLARE @NextVersion int
	UPDATE DbVersion 
	SET @NextVersion = DbVersion + 1, 
		DbVersion = DbVersion + 1,
		ModifiedBy = @ModifiedBy,
		ModifiedDate = GETDATE()
	SELECT @NextVersion");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[spGetNextDbVersion]");
        }
    }
}
