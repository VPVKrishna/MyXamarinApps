using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyUnityApp.Refit
{
    public interface ApiInterface
    {

        [Get("/todos/{id}")]
        Task<UserModel> GetUserData(int id);
    }
}
