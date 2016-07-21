using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603091327)]
    public class CreateSprocGetNextInstrumentVersion : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE PROCEDURE [dbo].[spGetNextInstrumentVersion]
                    @InstrumentId int,
					@ModifiedBy int
                AS
	SET NOCOUNT ON
	DECLARE @NextVersion int
	UPDATE InstrumentVersion 
	SET @NextVersion = DbVersion + 1, 
		DbVersion = DbVersion + 1,
		ModifiedBy = @ModifiedBy,
		ModifiedDate = GETDATE()
	WHERE InstrumentId = @InstrumentId
	SELECT @NextVersion");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[spGetNextInstrumentVersion]");
        }
    }
}
