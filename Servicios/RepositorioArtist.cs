using Dapper;
using Microsoft.Data.SqlClient;
using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Models;

namespace WebSpotifyClient.Servicios
{
    public class RepositorioArtist : IRepositorioArtist
    {
        private readonly string connectionString;

        public RepositorioArtist(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener registro
        public async Task<Artist> Obtener(string idSpotify)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Artist>(@"
                                    SELECT * FROM Artist WHERE IdSpotify = @IdSpotify"
                                    , idSpotify);
        }

        //Insertar-Actualiza registro
        public async Task Crear(Artist artist)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Sp_Artist_Create"
                                                    , new
                                                    {
                                                        Id = artist.Id,
                                                        IdSpotify = artist.IdSpotify,
                                                        Name = artist.Name,
                                                        Like = artist.Like,
                                                        ExternalUrl= artist.ExternalUrl,
                                                        Revisado = artist.Revisado
                                                    }
                                                    , commandType: System.Data.CommandType.StoredProcedure);
            artist.Id = id;
        }
    }
}
