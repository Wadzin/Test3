using Microsoft.Data.SqlClient;
using Partia.DTOs;

namespace Partia;

public interface IDbService
{
    Task<SqlConnection> GetConnection();
    
    Task<List<GetPolitykDTO>> GetPolitycy();
}