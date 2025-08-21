using Dapper;
using RCAWebApplication.Models;
using System.Data;
using System.Data.SqlClient;

namespace RCAWebApplication.Repository
{
    public class RcaAcRepository: IRcaAcRepository
    {
       // private readonly IDapperHelper _dapperHelper;
        private readonly string _conn;
        public RcaAcRepository(IConfiguration cfg)
        {
            _conn = cfg.GetConnectionString("DefaultConnection");
        }

        [Obsolete]
        public async Task<(IEnumerable<LookupItem>, IEnumerable<LookupItem>, IEnumerable<LookupItem>, IEnumerable<LookupItem>)> GetDropdownsAsync()
        {
            using var conn = new SqlConnection(_conn);
            using var multi = await conn.QueryMultipleAsync("dbo.sp_GetDropdowns", commandType: CommandType.StoredProcedure);
            return (await multi.ReadAsync<LookupItem>(), await multi.ReadAsync<LookupItem>(), await multi.ReadAsync<LookupItem>(), await multi.ReadAsync<LookupItem>());
        }
        public async Task AddNewItemAsync(string type, string name)
        {
            using var conn = new SqlConnection(_conn);
            await conn.ExecuteAsync("dbo.sp_AddNewItem", new { Type = type, Name = name }, commandType: CommandType.StoredProcedure);
        }
        public async Task AddSetupAsync(RcaAcSetupDto dto)
        {
            using var conn = new SqlConnection(_conn);
            await conn.ExecuteAsync("dbo.sp_AddRcaAcSetup", dto, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<RcaAcSetupRow>> GetSetupListAsync()
        {
            using var conn = new SqlConnection(_conn);
            return await conn.QueryAsync<RcaAcSetupRow>("dbo.sp_GetRcaAcSetup", commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<Combination>> ExporttoCSVRACSetupListAsync()
        {
            using var conn = new SqlConnection(_conn);
            return await conn.QueryAsync<Combination>("dbo.sp_GetRcaAcSetup", commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteAsync(int rcaId)
        {
            using var conn = new SqlConnection(_conn);
            var rowsAffected = await conn.ExecuteAsync("dbo.DeleteRCASetup", new { RCAId = rcaId },
               commandType: CommandType.StoredProcedure
           );
            return rowsAffected < 0;
        }

    }
}
