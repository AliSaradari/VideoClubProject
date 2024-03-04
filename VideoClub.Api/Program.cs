
using Microsoft.EntityFrameworkCore;
using VideoClub.Contracts.Interfaces;
using VideoClub.Persistence.EF;
using VideoClub.Persistence.EF.Genres;
using VideoClub.Services.Genres;
using VideoClub.Services.Genres.Contracts;

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
