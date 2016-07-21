using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603221027)]
    public class CreateSprocGetNextPartActionVersion : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE PROCEDURE [dbo].[spGetNextPartActionVersion]
                    @PartActionId int,
					@ModifiedBy int
                AS
	SET NOCOUNT ON
	DECLARE @NextVersion int
	UPDATE PartActionVersion 
	SET @NextVersion = DbVersion + 1, 
		DbVersion = DbVersion + 1,
		ModifiedBy = @ModifiedBy,
		ModifiedDate = GETDATE()
	WHERE PartActionId = @PartActionId
	SELECT @NextVersion");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[spGetNextPartActionVersion]");
        }
    }
}
