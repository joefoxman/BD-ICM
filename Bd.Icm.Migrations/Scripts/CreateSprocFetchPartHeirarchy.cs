using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603181250)]
    public class CreateSprocFetchPartHeirarchy : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE PROCEDURE [dbo].[spFetchPartHeirarchy]
	@PartId int,
	@Version int
AS
BEGIN
	SET NOCOUNT ON;
	WITH SubParts(InstrumentId, ParentPartId, PartId, Name, [Description], [Level])
	AS
	(
		SELECT InstrumentId, ParentPartId, Id, [Name], [Description], 0 AS [Level]
		FROM dbo.Part
		WHERE Id = @PartId AND dbo.fnIsInVersion(EffectiveFrom, EffectiveTo, @Version) = 1
		UNION ALL
		SELECT p.InstrumentId, p.ParentPartId, p.Id, p.[Name], p.[Description], [Level]+1
		FROM Part AS p
		INNER JOIN SubParts AS sp ON p.Id = sp.ParentPartId
		WHERE dbo.fnIsInVersion(EffectiveFrom, EffectiveTo, @Version) = 1
	)
	SELECT InstrumentId, ParentPartId, PartId, [Name], [Description], [Level]
	FROM SubParts AS s
	ORDER BY Level DESC
END
");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP PROCEDURE [dbo].[spFetchPartHeirarchy]");
        }
    }
}
