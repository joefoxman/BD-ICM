using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603291340)]
    public class CreateSprocspSearchInstrumentParts : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE PROCEDURE [dbo].[spSearchInstrumentParts](
	@InstrumentId int,
	@Version int,
	@SearchKey nvarchar(50)
)
AS
BEGIN
	WITH SubParts(ParentPartId, PartId, [Name], [Description], [DocumentNumber], [SerialNumber], [SapPartNumber], [Level])
	AS
	(
		SELECT 
			ParentPartId, 
			Part.Id, 
			[Name], 
			[Description], 
			[DocumentNumber], 
			[SerialNumber], 
			[SapPartNumber],
			0 AS [Level]
		FROM dbo.Part
		WHERE (ParentPartId IS NULL) 
			AND (InstrumentId = @InstrumentId) 
			AND dbo.fnIsInVersion(EffectiveFrom, EffectiveTo, @Version) = 1
		UNION ALL
		SELECT 
			p.ParentPartId, 
			p.Id, 
			p.[Name], 
			p.[Description], 
			p.[DocumentNumber], 
			p.[SerialNumber], 
			p.[SapPartNumber],
			[Level]+1
		FROM Part AS p
		INNER JOIN SubParts AS sp ON p.ParentPartId = sp.PartId
		WHERE dbo.fnIsInVersion(p.EffectiveFrom, p.EffectiveTo, @Version) = 1

	)
	SELECT DISTINCT *
	FROM SubParts 
	WHERE ((CHARINDEX(@SearchKey, [Name]) > 0) OR (CHARINDEX(@SearchKey, [Description]) > 0) OR (CHARINDEX(@SearchKey, [DocumentNumber]) > 0))

END
");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[spSearchInstrumentParts]");
        }
    }
}
