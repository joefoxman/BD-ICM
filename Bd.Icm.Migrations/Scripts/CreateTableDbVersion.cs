using System;
using FluentMigrator;

namespace Bd.Icm.Migrations.Scripts
{
    [Migration(201603231037)]
    public class CreateTableDbVersion : Migration
    {
        public override void Up()
        {
            Create.Table(DbSchema.Tables.DbVersion)
                .WithColumn(DbSchema.Columns.DbVersion).AsInt32().NotNullable()
                .WithColumn(DbSchema.Columns.ModifiedDate).AsDateTime().NotNullable()
                .WithColumn(DbSchema.Columns.ModifiedBy).AsInt32().NotNullable();

            Create.ForeignKey("FK_DbVersion_User_Modifier")
                .FromTable(DbSchema.Tables.DbVersion)
                .InSchema(DbSchema.Schema)
                .ForeignColumn(DbSchema.Columns.ModifiedBy)
                .ToTable(DbSchema.Tables.User)
                .InSchema(DbSchema.Schema)
                .PrimaryColumn("Id");

            //Insert.IntoTable(DbSchema.Tables.DbVersion).InSchema(DbSchema.Schema).Row(new
            //{
            //    DbVersion = 1,
            //    ModifiedDate = DateTime.Now,
            //    ModifiedBy = 1
            //});
        }

        public override void Down()
        {
            Delete.FromTable(DbSchema.Tables.DbVersion).AllRows();
            Delete.Table(DbSchema.Tables.DbVersion);
        }
    }
}
