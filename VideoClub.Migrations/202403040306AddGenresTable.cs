using FluentMigrator;

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
                .WithColumn("CreateAt").AsDateTime2().NotNullable();
        }
    }
}
