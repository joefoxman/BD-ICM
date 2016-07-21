using System;
using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201604142150)]
    public class CleanUpDataForNewCommitChanges : Migration
    {
        public override void Up()
        {
            Delete.FromTable(DbSchema.Tables.PartAction).AllRows();
            Delete.FromTable(DbSchema.Tables.PartActionVersion).AllRows();
            Delete.FromTable(DbSchema.Tables.PartMetadata).AllRows();
            Delete.FromTable(DbSchema.Tables.PartMetadataVersion).AllRows();
            Delete.FromTable(DbSchema.Tables.Part).AllRows();
            Delete.FromTable(DbSchema.Tables.PartVersion).AllRows();
            Delete.FromTable(DbSchema.Tables.InstrumentCommit).AllRows();
            Delete.FromTable(DbSchema.Tables.Instrument).AllRows();
            Delete.FromTable(DbSchema.Tables.InstrumentVersion).AllRows();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
