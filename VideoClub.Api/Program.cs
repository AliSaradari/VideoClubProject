
using Microsoft.EntityFrameworkCore;
using VideoClub.Contracts.Interfaces;
using VideoClub.Infrastructure;
using VideoClub.Persistence.EF;
using VideoClub.Persistence.EF.Genres;
using VideoClub.Persistence.EF.Movies;
using VideoClub.Services.Genres;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Movies;
using VideoClub.Services.Movies.Contracts;

namespace VideoClub.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Configuration.AddJsonFile("appsettings.json");

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<EFDataContext>(
                options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<UnitOfWork, EFUnitOfWork>();
            builder.Services.AddScoped<GenreService, GenreAppService>();
            builder.Services.AddScoped<GenreManagerService, GenreManagerAppService>();
            builder.Services.AddScoped<GenreRepository, EfGenreRepository>();
            builder.Services.AddScoped<DateTimeService, DateTimeAppService>();
            builder.Services.AddScoped<MovieService, MovieAppService>();
            builder.Services.AddScoped<MovieManagerService, MovieManagerAppService>();
            builder.Services.AddScoped<MovieRepository, EfMovieRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
