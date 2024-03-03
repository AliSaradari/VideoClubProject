using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Migrations
{
    [Migration(202403040306)]
    public class _202403040306AddGenresTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Genres");
        }

        public override void Up()
        {
            Create.Table("Genres")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("Rate").AsFloat().WithDefaultValue(0).NotNullable();            
        }
    }
}
