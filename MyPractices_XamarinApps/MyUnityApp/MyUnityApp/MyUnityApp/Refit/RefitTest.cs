using MyUnityApp.Refit;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyUnityApp
{
    public class RefitTest
    {
        private ApiInterface restApiService;
        public RefitTest(ApiInterface apiInterface)
        {
            restApiService = apiInterface;
        }

        public async System.Threading.Tasks.Task<UserModel> GetDataAsync()
        {
            UserModel userData = await restApiService.GetUserData(1);
            Console.Write("UserData : " + userData);
            return userData;
        }
    }
}
