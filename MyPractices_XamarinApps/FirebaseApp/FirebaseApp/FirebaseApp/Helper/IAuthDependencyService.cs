using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseApp.Helper
{
    public interface IAuthDependencyService
    {
        Task<string> LoginWithUsernameAndPasswordAsync(string username, string password);
        Task<string> RegisterWithUsernameAndPasswordAsync(string username, string password);
        Task<string> ChangePasswordAsync(string username, string oldPassword, string newPassword);
        Task<string> ChangePasswordAsync(object currentUser, string newPassword);
        Task<string> ChangePasswordAsync(string newPassword);
        Task ForgotPasswordAsync(string username);
        object GetCurrentUser();
        Task<IList<string>> FetchProvidersAsync(string username);

        void SendMobileCodeForVerification(string phone);
    }
}
