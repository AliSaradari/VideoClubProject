using FluentMigrator;

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
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("PublishYear").AsString().NotNullable()
                .WithColumn("Director").AsString().NotNullable()
                .WithColumn("Duration").AsInt32().NotNullable()
                .WithColumn("GenreId").AsInt32().NotNullable().ForeignKey("Fk_Genres_Movies", "Genres", "Id")
                .WithColumn("MinimumAllowedAge").AsInt32().NotNullable()
                .WithColumn("DailyRentalPrice").AsDecimal().NotNullable()
                .WithColumn("PenaltyRates").AsDecimal().NotNullable()
                .WithColumn("Count").AsInt32().NotNullable()
                .WithColumn("CreateAt").AsDateTime().NotNullable();




        }
    }
}
