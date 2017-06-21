using OneDriveIntergration.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneDriveIntergration.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> GetAuthenticationResult(string code);
        Task<IList<Document>> GetDocuments(string accessToken);

        #region Drives
        Task<string> GetDefaultDrive(string accessToken);
        Task<string> ListAvailableDrives(string accessToken);
        Task<string> ListSharedDrives(string accessToken);
        Task<string> ListRecentFiles(string accessToken);
        #endregion
    }
}
