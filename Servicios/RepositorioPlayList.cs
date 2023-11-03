using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace WebSpotifyClient.Servicios
{
    public class RepositorioPlayList : IRepositorioPlayList
    {
        private readonly string connectionString;

        public RepositorioPlayList(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener registro
        public async Task<PlayList> Obtener(string idSpotify)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<PlayList>(@"
                                    SELECT * FROM PlayList WHERE IdSpotify = @IdSpotify"
                                    , idSpotify);
        }
                
        //Insertar-Actualiza registro
        public async Task Crear(PlayList playList)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Sp_PlayList_Create"
                                                    , new { Id = playList.Id
                                                            , IdSpotify = playList.IdSpotify
                                                            , Name = playList.Name
                                                            , ExternalUrl = playList.ExternalUrl}                                            
                                                    , commandType: System.Data.CommandType.StoredProcedure);
            playList.Id = id;
        }

        //Obtiene vista principal
        public async Task<IEnumerable<PlayListView>> ObtenerPlaylistView(DateTime addedAt)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<PlayListView>("Sp_Playlist_View"
                                                        , new { AddedAt = addedAt }
                                                        , commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<DateTime>> ObtenerFechas()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<DateTime>(@"select distinct addedat from RelationPlayListTrack order by 1 desc");
        }

    }
}
