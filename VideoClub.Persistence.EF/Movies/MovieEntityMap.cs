using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoClub.Entities.Movies;

namespace VideoClub.Persistence.EF.Movies
{
    public class MovieEntityMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> _)
        {
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd();
            _.Property(_ => _.Title).IsRequired().HasMaxLength(50);
            _.Property(_ => _.Description).IsRequired().HasMaxLength(200);
            _.Property(_ => _.PublishYear).IsRequired().HasMaxLength(4);
            _.Property(_ => _.Director).IsRequired().HasMaxLength(50);
            _.Property(_ => _.Duration).IsRequired().HasMaxLength(5);
            _.Property(_ => _.GenreId).IsRequired();
            _.Property(_ => _.MinimumAllowedAge).IsRequired().HasMaxLength(5);
            _.Property(_ => _.DailyRentalPrice).IsRequired();
            _.Property(_ => _.PenaltyRates).IsRequired();
            _.Property(_ => _.Count).IsRequired();

            _.HasOne(_ => _.genre)
                .WithMany(_ => _.Movies)
                .HasForeignKey(_ => _.GenreId)
                .OnDelete(DeleteBehavior.NoAction);


            
        }
    }
}
