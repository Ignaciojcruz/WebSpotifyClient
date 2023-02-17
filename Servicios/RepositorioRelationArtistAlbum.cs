using Dapper;
using Microsoft.Data.SqlClient;
using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Models;

namespace WebSpotifyClient.Servicios
{
    public class RepositorioRelationArtistAlbum : IRepositorioRelationArtistAlbum
    {
        private readonly string connectionString;

        public RepositorioRelationArtistAlbum(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener registro
        public async Task<RelationArtistAlbum> Obtener(string idSpotify)
        {
            //TODO - revisar la llamada
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<RelationArtistAlbum>(@"
                                    SELECT * FROM RelationArtistAlbum WHERE IdSpotify = @IdSpotify"
                                    , idSpotify);
        }

        //Insertar-Actualiza registro
        public async Task Crear(RelationArtistAlbum model)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Sp_RelationArtistAlbum_Create"
                                                    , new
                                                    {
                                                        Id = model.Id,                                                            
                                                        IdArtist = model.IdArtist,                                                            
                                                        IdAlbum = model.IdAlbum                                                        
                                                    }
                                                    , commandType: System.Data.CommandType.StoredProcedure);
            model.Id = id;
        }
    }
}
