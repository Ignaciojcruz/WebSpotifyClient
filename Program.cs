using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IRepositorioPlayList, RepositorioPlayList>();
builder.Services.AddTransient<IRepositorioSpotify, RepositorioSpotify>();
builder.Services.AddTransient<IRepositorioTrack, RepositorioTrack>();
builder.Services.AddTransient<IRepositorioArtist, RepositorioArtist>();
builder.Services.AddTransient<IRepositorioAlbum, RepositorioAlbum>();
builder.Services.AddTransient<IRepositorioImage, RepositorioImage>();
builder.Services.AddTransient<IRepositorioRelationTrackArtist, RepositorioRelationTrackArtist>();
builder.Services.AddTransient<IRepositorioRelationPlayListTrack, RepositorioRelationPlayListTrack>();
builder.Services.AddTransient<IRepositorioRelationArtistAlbum, RepositorioRelationArtistAlbum>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
