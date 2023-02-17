using Dapper;
using Microsoft.Data.SqlClient;
using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Models;

namespace WebSpotifyClient.Servicios
{
    public class RepositorioImage : IRepositorioImage
    {
        private readonly string connectionString;

        public RepositorioImage(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Obtener registro
        public async Task<Image> Obtener(string idSpotify)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Image>(@"
                                    SELECT * FROM Image WHERE IdSpotify = @IdSpotify"
                                    , idSpotify);
        }

        //Insertar-Actualiza registro
        public async Task Crear(Image image)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Sp_Image_Create"
                                                    , new
                                                    {
                                                        Id = image.Id,
                                                        IdSpotify = image.IdSpotify,                                                            
                                                        Height = image.Height,
                                                        Url = image.Url,
                                                        Width = image.Width,
                                                        Tipo = image.Tipo
                                                    }
                                                    , commandType: System.Data.CommandType.StoredProcedure);
            image.Id = id;
        }
    }
}
