using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603250900)]
    public class CreateSprocFetchUncommittedPartChanges : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE PROCEDURE [dbo].[spFetchUncommittedPartChanges](
	@InstrumentId int,
	@UserId int
)
AS
BEGIN
	WITH SubParts(ParentPartId, PartId, [Name], [Description], [DrawingNumber], [DashNumber], [SerialNumber], [SapPartNumber], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], InstrumentCommitId, [ModificationStatus], [Level])
	AS
	(
		SELECT 
			ParentPartId, 
			Id, 
			[Name], 
			[Description], 
			[DrawingNumber], 
			[DashNumber], 
			[SerialNumber], 
			[SapPartNumber],
			[CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], 
			[InstrumentCommitId], 
			dbo.fnModificationStatus(Id, EffectiveFrom, EffectiveTo) AS ModificationStatus, 
			0 AS [Level]
		FROM dbo.Part
		WHERE (ParentPartId IS NULL) AND (InstrumentId = @InstrumentId) 
		UNION ALL
		SELECT 
			p.ParentPartId, 
			p.Id, 
			p.[Name], 
			p.[Description], 
			p.[DrawingNumber], 
			p.[DashNumber], 
			p.[SerialNumber], 
			p.[SapPartNumber],
			p.[CreatedBy], p.[CreatedDate], p.[ModifiedBy], p.[ModifiedDate], 
			p.[InstrumentCommitId], 
			dbo.fnModificationStatus(p.Id, p.EffectiveFrom, p.EffectiveTo) AS ModificationStatus, 
			[Level]+1
		FROM Part AS p
		INNER JOIN SubParts AS sp ON p.ParentPartId = sp.PartId
	)
	SELECT DISTINCT *
	FROM SubParts AS s
	WHERE InstrumentCommitId IS NULL AND (CreatedBy = @UserId) AND (ModifiedBy = @UserId)
END");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[spFetchUncommittedPartChanges]");
        }
    }
}
