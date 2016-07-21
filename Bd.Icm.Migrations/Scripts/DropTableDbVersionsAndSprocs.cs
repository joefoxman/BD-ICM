using System.Collections.Generic;
using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603231137)]
    public class DropTableDbVersionsAndSprocs : Migration
    {
        public override void Up()
        {
            Delete.Column(DbSchema.Columns.DbVersion)
                .FromTable(DbSchema.Tables.InstrumentVersion)
                .InSchema(DbSchema.Schema);
            Delete.Column(DbSchema.Columns.DbVersion)
                .FromTable(DbSchema.Tables.PartVersion)
                .InSchema(DbSchema.Schema);
            Delete.Column(DbSchema.Columns.DbVersion)
                .FromTable(DbSchema.Tables.PartActionVersion)
                .InSchema(DbSchema.Schema);
            Delete.Column(DbSchema.Columns.DbVersion)
                .FromTable(DbSchema.Tables.PartMetadataVersion)
                .InSchema(DbSchema.Schema);

            Execute.Sql("DROP PROCEDURE [dbo].[spGetNextInstrumentVersion]");
            Execute.Sql("DROP PROCEDURE [dbo].[spGetNextPartVersion]");
            Execute.Sql("DROP PROCEDURE [dbo].[spGetNextPartActionVersion]");
        }

        public override void Down()
        {

            Alter.Table(DbSchema.Tables.InstrumentVersion)
                .AddColumn(DbSchema.Columns.DbVersion)
                .AsInt32()
                .NotNullable()
                .WithDefaultValue(0);

            Alter.Table(DbSchema.Tables.PartVersion)
                .AddColumn(DbSchema.Columns.DbVersion)
                .AsInt32()
                .NotNullable()
                .WithDefaultValue(0);

            Alter.Table(DbSchema.Tables.PartActionVersion)
                .AddColumn(DbSchema.Columns.DbVersion)
                .AsInt32()
                .NotNullable()
                .WithDefaultValue(0);

            Alter.Table(DbSchema.Tables.PartMetadataVersion)
                .AddColumn(DbSchema.Columns.DbVersion)
                .AsInt32()
                .NotNullable()
                .WithDefaultValue(0);

            var migrators = new List<Migration>
            {
                new CreateSprocGetNextInstrumentVersion(),
                new CreateSprocGetNextPartVersion(),
                new CreateSprocGetNextPartActionVersion()
            };
            foreach (var migrator in migrators)
            {
                migrator.Up();
            }
        }
    }
}
