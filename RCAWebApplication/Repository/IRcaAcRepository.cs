using RCAWebApplication.Models;

namespace RCAWebApplication.Repository
{
    public interface IRcaAcRepository
    {
        Task<(IEnumerable<LookupItem> cats, IEnumerable<LookupItem> denials, IEnumerable<LookupItem> rcas, IEnumerable<LookupItem> actions)> GetDropdownsAsync();
        Task AddNewItemAsync(string type, string name);
        Task AddSetupAsync(RcaAcSetupDto dto);
        Task<IEnumerable<RcaAcSetupRow>> GetSetupListAsync();
        Task<IEnumerable<Combination>> ExporttoCSVRACSetupListAsync();
        Task<bool> DeleteAsync(int rcaId);
    }
}
