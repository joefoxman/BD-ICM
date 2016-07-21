using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603250859)]
    public class CreateUdfModificationStatus : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE FUNCTION [dbo].[fnModificationStatus]
(
	@PartId int,
	@EffectiveFrom int,
	@EffectiveTo int
)
RETURNS int
AS
BEGIN 
	IF (@EffectiveTo <> 2147483647)
		BEGIN
			DECLARE @NewerVersions int
			SELECT @NewerVersions = COUNT(*) FROM Part WHERE Part.Id = @PartId AND Part.EffectiveFrom >= @EffectiveTo
			IF (@NewerVersions = 0) RETURN 3
		END

	DECLARE @PreviousVersions int
	SELECT @PreviousVersions = COUNT(*) FROM Part WHERE Part.Id = @PartId AND Part.EffectiveTo <= @EffectiveFrom
	IF (@PreviousVersions = 0)
		RETURN 1
	RETURN 2
END");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP FUNCTION [dbo].[fnModificationStatus]");
        }
    }
}
