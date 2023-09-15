using Dapper;
using Microsoft.Data.SqlClient;
using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Models;

namespace WebSpotifyClient.Servicios
{
    public class RepositorioRelationAlbumTrack : IRepositorioRelationAlbumTrack
    {
        private readonly string connectionString;

        public RepositorioRelationAlbumTrack(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Insertar-Actualiza registro
        public async Task Crear(RelationAlbumTrack model)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Sp_RelationAlbumTrack_Create"
                                                    , new
                                                    {
                                                        Id = model.Id,
                                                        IdAlbum = model.IdAlbum,
                                                        IdTrack = model.IdTrack                                                        
                                                    }
                                                    , commandType: System.Data.CommandType.StoredProcedure);
            model.Id = id;
        }
    }
}
