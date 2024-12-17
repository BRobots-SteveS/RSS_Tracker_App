using Rss_Tracking_Data;
using Microsoft.EntityFrameworkCore;
using Rss_Tracking_Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<Rss_TrackingDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:RssTrackerConnectionString"]);
#if DEBUG
    opts.EnableSensitiveDataLogging();
#endif
});
builder.Services
    .AddScoped<Rss_TrackingDbContext>()
    .AddScoped<IAuthorRepository, AuthorRepository>()
    .AddScoped<IEpisodeRepository, EpisodeRepository>()
    .AddScoped<IFeedRepository, FeedRepository>()
    .AddScoped<IUserFavoriteRepository, UserFavoriteRepository>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddLogging(options =>
    {
        options.AddDebug();
        options.AddConsole();
    });

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
