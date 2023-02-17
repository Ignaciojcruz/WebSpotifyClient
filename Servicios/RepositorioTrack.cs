using Dapper;
using Microsoft.Data.SqlClient;
using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Models;

namespace WebSpotifyClient.Servicios
{
    public class RepositorioTrack : IRepositorioTrack
    {
        private readonly string connectionString;

        public RepositorioTrack(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener registro
        public async Task<Track> Obtener(string idSpotify)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Track>(@"
                                    SELECT * FROM Track WHERE IdSpotify = @IdSpotify"
                                    , idSpotify);
        }

        //Insertar-Actualiza registro
        public async Task Crear(Track track)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Sp_Track_Create"
                                                    , new
                                                    {
                                                        Id = track.Id,
                                                        IdSpotify = track.IdSpotify,
                                                        Name = track.Name,
                                                        ExternalUrl = track.ExternalUrl

                                                    }
                                                    , commandType: System.Data.CommandType.StoredProcedure);
            track.Id = id;
        }
    }
}
