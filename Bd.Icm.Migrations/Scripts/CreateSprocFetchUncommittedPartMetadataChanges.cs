using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604142147)]
    public class CreateSprocFetchUncommittedPartMetadataChanges : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE PROCEDURE [dbo].[spFetchUncommittedMetadataChanges](
	@InstrumentId int
)
AS
BEGIN
	WITH Metadata(PartMetadataId, PartId, Name, [Description], 
		[CreatedBy], [CreatedDate], [Creator], [ModifiedBy], [ModifiedDate], [Modifier],
		[MetaKey], [MetaValue], [InstrumentCommitId], [ModificationType], [RowVersion], [Level])
	AS
	(
		SELECT 
			PM1.Id, 
			P1.Id, 
			[Name], 
			[Description], 
			PM1.[CreatedBy], PM1.[CreatedDate], Creator.[UserName] AS Creator,
			PM1.[ModifiedBy], PM1.[ModifiedDate], Modifier.[UserName] As Modifier,
			PM1.[MetaKey], PM1.[MetaValue], PM1.[InstrumentCommitId], PM1.[ModificationType], PM1.[RowVersion], 0 AS [Level]
		FROM dbo.Part P1
		JOIN dbo.PartMetadata PM1 ON PM1.PartId = P1.Id AND PM1.EffectiveTo <= P1.EffectiveTo
		JOIN [User] AS Creator ON Creator.Id = PM1.CreatedBy
		JOIN [User] AS Modifier ON Modifier.Id = PM1.ModifiedBy
		WHERE (ParentPartId IS NULL) AND (InstrumentId = @InstrumentId) AND (PM1.InstrumentCommitId IS NULL)
		UNION ALL
		SELECT 
			PM2.Id,
			P2.Id, 
			P2.[Name], 
			P2.[Description], 
			PM2.[CreatedBy], PM2.[CreatedDate], Creator.[UserName] AS Creator,
			PM2.[ModifiedBy], PM2.[ModifiedDate], Modifier.[UserName] As Modifier,
			PM2.[MetaKey], PM2.[MetaValue], PM2.[InstrumentCommitId], PM2.[ModificationType], PM2.[RowVersion], [Level]+1
		FROM Part AS P2
		JOIN dbo.PartMetadata PM2 ON PM2.PartId = P2.Id
		JOIN [User] AS Creator ON Creator.Id = PM2.CreatedBy
		JOIN [User] AS Modifier ON Modifier.Id = PM2.ModifiedBy
		INNER JOIN Metadata AS MD ON P2.ParentPartId = MD.PartId
		WHERE (PM2.InstrumentCommitId IS NULL) AND (PM2.EffectiveTo <= P2.EffectiveTo)
	)
	SELECT *
	FROM Metadata AS s

END");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[spFetchUncommittedPartMetadataChanges]");
        }
    }
}
