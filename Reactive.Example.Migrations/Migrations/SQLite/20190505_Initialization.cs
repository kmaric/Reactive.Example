using System;
using FluentMigrator;
using Microsoft.EntityFrameworkCore;

namespace Reactive.Example.Migrations.Migrations.SQLite
{
    [Migration(2019050501)]
    public class SQLiteInitialization : Migration
    {
        public override void Up()
        {
            Create.Table("catalogue").InSchema("public")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("x_value").AsInt32()
                .WithColumn("y_value").AsInt32();
        }

        public override void Down()
        {
            Delete.FromTable("catalogue");
        }
    }
    
    [Migration(2019050502)]
    public class AddTimestampToCatalogue : Migration
    {
        public override void Up()
        {
            Alter.Table("catalogue").InSchema("public")
                .AddColumn("timestamp").AsDateTime().WithDefaultValue(DateTime.UtcNow);
        }

        public override void Down()
        {
            Delete.FromTable("catalogue");
        }
    }
}