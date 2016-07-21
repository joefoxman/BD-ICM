using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603291148)]
    public class AlterTablePartRenameDrawingNumberToDocumentNumber : Migration
    {
        public override void Up()
        {
            Rename.Column("DrawingNumber").OnTable(DbSchema.Tables.Part).To("DocumentNumber");
        }

        public override void Down()
        {
            Rename.Column("DocumentNumber").OnTable(DbSchema.Tables.Part).To("DrawingNumber");
        }
    }
}
