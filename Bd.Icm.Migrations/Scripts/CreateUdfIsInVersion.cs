using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603181249)]
    public class CreateUdfIsInVersion : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE FUNCTION [dbo].[fnIsInVersion]
(
	@EffectiveFrom int,
	@EffectiveTo int,
	@Version int
)
RETURNS bit
AS
BEGIN 
	IF (((@EffectiveTo = 2147483647) AND (@Version = 2147483647))
		 OR (@Version >= @EffectiveFrom AND @Version < @EffectiveTo))
		RETURN 1
	RETURN 0
END");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP FUNCTION [dbo].[fnIsInVersion]");
        }
    }
}
