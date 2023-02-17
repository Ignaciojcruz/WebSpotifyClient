using Dapper;
using Microsoft.Data.SqlClient;
using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Models;

namespace WebSpotifyClient.Servicios
{
    public class RepositorioRelationPlayListTrack : IRepositorioRelationPlayListTrack
    {
        private readonly string connectionString;

        public RepositorioRelationPlayListTrack(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener registro
        public async Task<RelationPlayListTrack> Obtener(string idSpotify)
        {
            //TODO - revisar la busqueda
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<RelationPlayListTrack>(@"
                                    SELECT * FROM RelationPlayListTrack WHERE IdSpotify = @IdSpotify"
                                    , idSpotify);
        }

        //Insertar-Actualiza registro
        public async Task Crear(RelationPlayListTrack model)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Sp_RelationPlayListTrack_Create"
                                                    , new
                                                    {
                                                        Id = model.Id,
                                                        IdTrack = model.IdTrack,                                                            
                                                        IdPlayList = model.idPlayList,                                                            
                                                        AddedAt= model.AddedAt
                                                    }
                                                    , commandType: System.Data.CommandType.StoredProcedure);
            model.Id = id;
        }
    }
}
