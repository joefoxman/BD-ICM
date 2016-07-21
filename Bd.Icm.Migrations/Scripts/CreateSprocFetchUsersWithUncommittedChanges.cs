using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603241700)]
    public class CreateSprocFetchUsersWithUncommittedChanges : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE PROCEDURE [dbo].[spFetchUsersWithUncommitedChanges]
	@InstrumentId int
AS
BEGIN
	SET NOCOUNT ON;
	WITH SubParts(ParentPartId, PartId, Name, [Description], [ModifiedBy], [Level])
	AS
	(
		SELECT ParentPartId, Id, [Name], [Description], [ModifiedBy], 0 AS [Level]
		FROM dbo.Part
		WHERE (ParentPartId IS NULL) AND (InstrumentId = @InstrumentId) AND (InstrumentCommitId IS NULL)
		UNION ALL
		SELECT p.ParentPartId, p.Id, p.[Name], p.[Description], p.[ModifiedBy], [Level]+1
		FROM Part AS p
		INNER JOIN SubParts AS sp ON p.ParentPartId = sp.PartId
	)
	SELECT DISTINCT
		u.Id,
		u.FirstName,
		u.LastName
	FROM SubParts AS s
	JOIN [User] as u ON u.Id = s.ModifiedBy
END");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[spFetchUsersWithUncommitedChanges]");
        }
    }
}
