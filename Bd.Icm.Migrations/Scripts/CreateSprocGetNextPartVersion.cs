using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603141141)]
    public class CreateSprocGetNextPartVersion : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE PROCEDURE [dbo].[spGetNextPartVersion]
                    @PartId int,
					@ModifiedBy int
                AS
	SET NOCOUNT ON
	DECLARE @NextVersion int
	UPDATE PartVersion 
	SET @NextVersion = DbVersion + 1, 
		DbVersion = DbVersion + 1,
		ModifiedBy = @ModifiedBy,
		ModifiedDate = GETDATE()
	WHERE PartId = @PartId
	SELECT @NextVersion");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[spGetNextPartVersion]");
        }
    }
}
