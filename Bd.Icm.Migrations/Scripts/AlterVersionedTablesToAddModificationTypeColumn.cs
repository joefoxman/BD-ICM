using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603251009)]
    public class AlterVersionedTablesToAddModificationTypeColumn : Migration
    {
        public override void Up()
        {
            Alter.Table(DbSchema.Tables.Instrument).AddColumn(DbSchema.Columns.ModificationType).AsInt32().NotNullable().WithDefaultValue(0);
            Alter.Table(DbSchema.Tables.Part).AddColumn(DbSchema.Columns.ModificationType).AsInt32().NotNullable().WithDefaultValue(0);
            Alter.Table(DbSchema.Tables.PartAction).AddColumn(DbSchema.Columns.ModificationType).AsInt32().NotNullable().WithDefaultValue(0);
            Alter.Table(DbSchema.Tables.PartMetadata).AddColumn(DbSchema.Columns.ModificationType).AsInt32().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            Delete.Column(DbSchema.Columns.ModificationType).FromTable(DbSchema.Tables.Instrument);
            Delete.Column(DbSchema.Columns.ModificationType).FromTable(DbSchema.Tables.Part);
            Delete.Column(DbSchema.Columns.ModificationType).FromTable(DbSchema.Tables.PartAction);
            Delete.Column(DbSchema.Columns.ModificationType).FromTable(DbSchema.Tables.PartMetadata);
        }
    }
}
