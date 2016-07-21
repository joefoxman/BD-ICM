using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Bd.Icm.Migrations.Scripts
{
    public static class MigrationExtensions
    {
        public static void CreateUserForeignKeys(this Migration migrator, string fromTable)
        {
            migrator.Create.ForeignKey($"FK_{fromTable}_User_Creator")
                .FromTable(fromTable)
                .InSchema(DbSchema.Schema)
                .ForeignColumn(DbSchema.Columns.CreatedBy)
                .ToTable(DbSchema.Tables.User)
                .InSchema(DbSchema.Schema)
                .PrimaryColumn("Id");
            migrator.Create.ForeignKey($"FK_{fromTable}_User_Modifier")
                .FromTable(fromTable)
                .InSchema(DbSchema.Schema)
                .ForeignColumn(DbSchema.Columns.ModifiedBy)
                .ToTable(DbSchema.Tables.User)
                .InSchema(DbSchema.Schema)
                .PrimaryColumn("Id");
        }

        public static ICreateTableWithColumnOrSchemaOrDescriptionSyntax WithCommitFields(this ICreateTableWithColumnOrSchemaOrDescriptionSyntax migrator)
        {
            return migrator.WithColumn("CommittedDate").AsDateTime().Nullable();
        }

        public static ICreateTableWithColumnOrSchemaOrDescriptionSyntax WithAuditFields(this ICreateTableWithColumnOrSchemaOrDescriptionSyntax migrator)
        {
            return migrator.WithColumn(DbSchema.Columns.CreatedDate).AsDateTime().NotNullable()
                .WithColumn(DbSchema.Columns.CreatedBy).AsInt32().NotNullable()
                .WithColumn(DbSchema.Columns.ModifiedDate).AsDateTime().NotNullable()
                .WithColumn(DbSchema.Columns.ModifiedBy).AsInt32().NotNullable();
        }

        public static ICreateTableWithColumnOrSchemaOrDescriptionSyntax WithRowVersionTimestamp(this ICreateTableWithColumnOrSchemaOrDescriptionSyntax migrator)
        {
            return migrator.WithColumn(DbSchema.Columns.RowVersion).AsCustom("rowversion").NotNullable();
        }

        public static ICreateTableWithColumnOrSchemaOrDescriptionSyntax WithRowVersionIdentity(this ICreateTableWithColumnOrSchemaOrDescriptionSyntax migrator)
        {
            return migrator.WithColumn(DbSchema.Columns.RowVersion).AsInt32().Identity().NotNullable().PrimaryKey();
        }

        public static ICreateTableWithColumnOrSchemaOrDescriptionSyntax WithVersioningFields(this ICreateTableWithColumnOrSchemaOrDescriptionSyntax migrator)
        {
            return migrator.WithColumn(DbSchema.Columns.EffectiveFrom).AsInt32().NotNullable()
                .WithColumn(DbSchema.Columns.EffectiveTo).AsInt32().NotNullable().WithDefaultValue(int.MaxValue);
        }

        public static void DeleteUserForeignKeys(this Migration migrator, string onTable)
        {
            migrator.Delete.ForeignKey($"FK_{onTable}_User_Creator");
            migrator.Delete.ForeignKey($"FK_{onTable}_User_Modifier");
        }
    }
}
