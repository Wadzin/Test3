using System.Data;
using Microsoft.Data.SqlClient;
using Partia.DTOs;

namespace Partia;

public class DbService : IDbService
{
    private readonly IConfiguration _configuration;

    public DbService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<SqlConnection> GetConnection()
    {
        var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }
        
        return connection;
    }

    public async Task<List<GetPolitykDTO>> GetPolitycy()
    {
        await using var connection = await GetConnection();

        var sql = @"SELECT
                    p.ID as id,
                    p.Imie as imie,
                    p.Nazwisko as nazwisko,
                    p.Powiedzenie as powiedzenie,
                    pa.Nazwa as nazwa,
                    pa.Skrot as skrot,
                    pa.DataZalozenia as dataZalozenia,
                    pr.Od as od,
                    pr.Do as do
                    FROM Polityk as p
                    JOIN Przynaleznosc pr ON p.ID = pr.Polityk_ID
                    JOIN Partia pa ON pa.ID = pr.Partia_ID";
        
        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();
        var politycy = new List<GetPolitykDTO>();

        while (await reader.ReadAsync())
        {
            var id = reader.GetInt32(reader.GetOrdinal("id"));
            
            var polityk = politycy.FirstOrDefault(p => p.Id == id);

            if (polityk == null)
            {
                polityk = new GetPolitykDTO()
                {
                    Id = id,
                    Imie = reader.GetString(reader.GetOrdinal("imie")),
                    Nazwisko = reader.GetString(reader.GetOrdinal("nazwisko")),
                    Powiedzenie = reader.IsDBNull(reader.GetOrdinal("powiedzenie"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("powiedzenie")),
                    Przynaleznosc = new List<GetPrzynaleznoscDTO>()
                };
                politycy.Add(polityk);
            }

            var Przynaleznosc = new GetPrzynaleznoscDTO
            {
                Nazwa = reader.GetString(reader.GetOrdinal("nazwa")),
                Skrot = reader.GetString(reader.GetOrdinal("skrot")),
                DataZalozenia = reader.GetDateTime(reader.GetOrdinal("dataZalozenia")),
                Od = reader.GetDateTime(reader.GetOrdinal("od")),
                Do = reader.IsDBNull(reader.GetOrdinal("do")) ? null : reader.GetDateTime(reader.GetOrdinal("do"))
            };
            
            polityk.Przynaleznosc.Add(Przynaleznosc);
        }
        
        return politycy;

    }
    
}