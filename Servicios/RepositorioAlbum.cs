using Dapper;
using Microsoft.Data.SqlClient;
using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Models;

namespace WebSpotifyClient.Servicios
{
    public class RepositorioAlbum : IRepositorioAlbum
    {
        private readonly string connectionString;

        public RepositorioAlbum(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener registro
        public async Task<Album> Obtener(string idSpotify)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Album>(@"
                                    SELECT * FROM Album WHERE IdSpotify = @IdSpotify"
                                    , idSpotify);
        }

        //Insertar-Actualiza registro
        public async Task Crear(Album album)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Sp_Album_Create"
                                                    , new
                                                    {
                                                        Id = album.Id,
                                                        IdSpotify = album.IdSpotify,
                                                        Name = album.Name,
                                                        ExternalUrl= album.ExternalUrl,
                                                    }
                                                    , commandType: System.Data.CommandType.StoredProcedure);
            album.Id = id;
        }
    }
}
