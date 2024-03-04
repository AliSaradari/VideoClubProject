using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Migrations
{
    [Migration(202404030344)]
    public class _202404030344AddMoviesTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Movies");
        }

        public override void Up()
        {
            Create.Table("Movies")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("Description").AsString(200).NotNullable()
                .WithColumn("PublishYear").AsString(4).NotNullable()
                .WithColumn("Director").AsString(50).NotNullable()
                .WithColumn("Duration").AsInt32().NotNullable()
                .WithColumn("GenreId").AsInt32().NotNullable().ForeignKey("Fk_Genres_Movies", "Genres", "Id")
                .WithColumn("MinimumAllowedAge").AsInt32().NotNullable()
                .WithColumn("DailyRentalPrice").AsDecimal().NotNullable()
                .WithColumn("PenaltyRates").AsDecimal().NotNullable()
                .WithColumn("Count").AsInt32().NotNullable()
                .WithColumn("Rate").AsFloat().NotNullable();




        }
    }
}
