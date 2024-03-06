using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoClub.Entities.Genres;

namespace VideoClub.Persistence.EF.Genres
{
    public class GenreEntityMap : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> _)
        {
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).ValueGeneratedOnAdd();
            _.Property(_ => _.Title).IsRequired().HasMaxLength(50);
        }
    }
}
