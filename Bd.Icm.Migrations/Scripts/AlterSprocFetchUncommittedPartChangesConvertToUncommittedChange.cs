using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604142146)]
    public class AlterSprocFetchUncommittedPartChangesConvertToUncommittedChange
        : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"ALTER PROCEDURE [dbo].[spFetchUncommittedPartChanges](
	@InstrumentId int
)
AS
BEGIN
	WITH SubParts(ParentPartId, PartId, [Name], [Description], [DocumentNumber], [DashNumber], 
		[SerialNumber], [SapPartNumber], [CreatedBy], [CreatedDate], [Creator], [ModifiedBy], [ModifiedDate], [Modifier], 
		InstrumentCommitId, [ModificationType], [RowVersion], [Level])
	AS
	(
		SELECT 
			ParentPartId, 
			Part.Id, 
			[Name], 
			[Description], 
			[DocumentNumber], 
			[DashNumber], 
			[SerialNumber], 
			[SapPartNumber],
			Part.[CreatedBy], Part.[CreatedDate], Creator.[UserName] AS Creator,
			Part.[ModifiedBy], Part.[ModifiedDate], Modifier.[UserName] As Modifier,
			[InstrumentCommitId], 
			Part.ModificationType, 
			Part.[RowVersion],
			0 AS [Level]
		FROM dbo.Part
		JOIN [User] AS Creator ON Creator.Id = Part.CreatedBy
		JOIN [User] AS Modifier ON Modifier.Id = Part.ModifiedBy
		WHERE (ParentPartId IS NULL) AND (InstrumentId = @InstrumentId) 
		UNION ALL
		SELECT 
			p.ParentPartId, 
			p.Id, 
			p.[Name], 
			p.[Description], 
			p.[DocumentNumber], 
			p.[DashNumber], 
			p.[SerialNumber], 
			p.[SapPartNumber],
			p.[CreatedBy], p.[CreatedDate], Creator2.UserName AS Creator,
			p.[ModifiedBy], p.[ModifiedDate], Modifier2.UserName AS Modifier,
			p.[InstrumentCommitId], 
			p.ModificationType, 
			p.[RowVersion],
			[Level]+1
		FROM Part AS p
		JOIN [User] AS Creator2 ON Creator2.Id = p.CreatedBy
		JOIN [User] AS Modifier2 ON Modifier2.Id = p.ModifiedBy
		INNER JOIN SubParts AS sp ON p.ParentPartId = sp.PartId
	)
	SELECT DISTINCT *
	FROM SubParts AS s
	WHERE InstrumentCommitId IS NULL
END");
        }

        public override void Down()
        {
            Execute.Sql(@"ALTER PROCEDURE [dbo].[spFetchUncommittedPartChanges](
	@InstrumentId int,
	@UserId int
)
AS
BEGIN
	WITH SubParts(ParentPartId, PartId, [Name], [Description], [DocumentNumber], [DashNumber], 
		[SerialNumber], [SapPartNumber], [CreatedBy], [CreatedDate], [Creator], [ModifiedBy], [ModifiedDate], [Modifier], 
		InstrumentCommitId, [ModificationStatus], [RowVersion], [Level])
	AS
	(
		SELECT 
			ParentPartId, 
			Part.Id, 
			[Name], 
			[Description], 
			[DocumentNumber], 
			[DashNumber], 
			[SerialNumber], 
			[SapPartNumber],
			Part.[CreatedBy], Part.[CreatedDate], Creator.[UserName] AS Creator,
			Part.[ModifiedBy], Part.[ModifiedDate], Modifier.[UserName] As Modifier,
			[InstrumentCommitId], 
			dbo.fnModificationStatus(Part.Id, EffectiveFrom, EffectiveTo) AS ModificationStatus, 
			Part.[RowVersion],
			0 AS [Level]
		FROM dbo.Part
		JOIN [User] AS Creator ON Creator.Id = Part.CreatedBy
		JOIN [User] AS Modifier ON Modifier.Id = Part.ModifiedBy
		WHERE (ParentPartId IS NULL) AND (InstrumentId = @InstrumentId) 
		UNION ALL
		SELECT 
			p.ParentPartId, 
			p.Id, 
			p.[Name], 
			p.[Description], 
			p.[DocumentNumber], 
			p.[DashNumber], 
			p.[SerialNumber], 
			p.[SapPartNumber],
			p.[CreatedBy], p.[CreatedDate], Creator2.UserName AS Creator,
			p.[ModifiedBy], p.[ModifiedDate], Modifier2.UserName AS Modifier,
			p.[InstrumentCommitId], 
			dbo.fnModificationStatus(p.Id, p.EffectiveFrom, p.EffectiveTo) AS ModificationStatus, 
			p.[RowVersion],
			[Level]+1
		FROM Part AS p
		JOIN [User] AS Creator2 ON Creator2.Id = p.CreatedBy
		JOIN [User] AS Modifier2 ON Modifier2.Id = p.ModifiedBy
		INNER JOIN SubParts AS sp ON p.ParentPartId = sp.PartId
	)
	SELECT DISTINCT *
	FROM SubParts AS s
	WHERE InstrumentCommitId IS NULL
END");
        }

    }
}
