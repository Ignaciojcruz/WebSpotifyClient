using Dapper;
using Microsoft.Data.SqlClient;
using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Models;

namespace WebSpotifyClient.Servicios
{
    public class RepositorioRelationTrackArtist : IRepositorioRelationTrackArtist
    {
        private readonly string connectionString;

        public RepositorioRelationTrackArtist(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener registro
        public async Task<RelationTrackArtist> Obtener(string idSpotify)
        {
            //TODO - definir el filtro
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<RelationTrackArtist>(@"
                                    SELECT * FROM RelationTrackArtist "
                                    , idSpotify);
        }

        //Insertar-Actualiza registro
        public async Task Crear(RelationTrackArtist model)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Sp_RelationTrackArtist_Create"
                                                    , new
                                                    {
                                                        Id = model.Id,                                                            
                                                        IdTrack = model.IdTrack,                                                            
                                                        idArtist = model.IdArtist                                                        
                                                    }
                                                    , commandType: System.Data.CommandType.StoredProcedure);
            model.Id = id;
        }
    }
}
