using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604142148)]
    public class AlterSprocFetchUsersWithUncommittedChangesIncludeMetadataChanges : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"ALTER PROCEDURE [dbo].[spFetchUsersWithUncommitedChanges]
	@InstrumentId int
AS
BEGIN
	SET NOCOUNT ON;
	WITH Metadata(ParentPartId, PartId, Name, [Description], [ModifiedBy], [MetaKey], [InstrumentCommitId], [Level])
	AS
	(
		SELECT ParentPartId, P1.Id, [Name], [Description], PM1.[ModifiedBy], PM1.[MetaKey], PM1.[InstrumentCommitId], 0 AS [Level]
		FROM dbo.Part P1
		JOIN dbo.PartMetadata PM1 ON PM1.PartId = P1.Id AND PM1.EffectiveTo <= P1.EffectiveTo
		WHERE (ParentPartId IS NULL) AND (InstrumentId = @InstrumentId) AND (PM1.InstrumentCommitId IS NULL)
		UNION ALL
		SELECT P2.ParentPartId, P2.Id, P2.[Name], P2.[Description], P2.[ModifiedBy], PM2.[MetaKey], PM2.[InstrumentCommitId], [Level]+1
		FROM Part AS P2
		JOIN dbo.PartMetadata PM2 ON PM2.PartId = P2.Id
		INNER JOIN Metadata AS MD ON P2.ParentPartId = MD.PartId
		WHERE (PM2.InstrumentCommitId IS NULL) AND (PM2.EffectiveTo <= P2.EffectiveTo)
	), 
	SubParts(ParentPartId, PartId, Name, [Description], [ModifiedBy], [Level])
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
	UNION
		SELECT DISTINCT
			u.Id,
			u.FirstName,
			u.LastName
		FROM Metadata AS s2
		JOIN [User] as u ON u.Id = s2.ModifiedBy
END");
        }

        public override void Down()
        {
            Execute.Sql(@"ALTER PROCEDURE [dbo].[spFetchUsersWithUncommitedChanges]
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
    }
}
