using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using System.Collections.ObjectModel;
using Firebase.Database.Offline;

namespace FirebaseApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DbPage : ContentPage
    {
        // Reference link: https://www.c-sharpcorner.com/article/xamarin-forms-working-with-firebase-realtime-database-crud-operations/
        // Reference link: https://github.com/cabauman/firebase-database-dotnet/tree/xamarin_forms_sample
        private FirebaseClient fbClient;
        private List<User> ListOfUsers = new List<User>();
        private ObservableCollection<User> collection;
        private bool IsRunningProgress { set; get; }
        
        const string USERS = "Users";
        public DbPage()
        {
            InitializeComponent();
            BindingContext = this;
            var options = new FirebaseOptions
            {
                OfflineDatabaseFactory = (t, s) => new OfflineDatabase(t, s)
            };
            fbClient = new FirebaseClient("https://fir-app-b1cb2.firebaseio.com/", options);
            collection = new ObservableCollection<User>(ListOfUsers);
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            IsRunningProgress = !IsRunningProgress;


            if (string.IsNullOrWhiteSpace(UserId.Text))
            {
                ShowAlert("Id should not be empty...!");
                return;
            }
            if (string.IsNullOrWhiteSpace(UserName.Text))
            {
                ShowAlert("Name should not be empty...!");
                return;
            }
            int id = Convert.ToInt32(UserId.Text);
            string name = UserName.Text;
            try
            {
                await AddUsersDetails(id, name);
                setUI();
            }
            catch (Exception)
            {
                ShowAlert("Error in adding data. Please check network connectivity..!");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserId.Text))
            {
                ShowAlert("Id should not be empty...!");
                return;
            }
            if (string.IsNullOrWhiteSpace(UserName.Text))
            {
                ShowAlert("Name should not be empty...!");
                return;
            }
            int id = Convert.ToInt32(UserId.Text);
            string name = UserName.Text;
            try
            {
                await UpdateUserDetails(id, name);
                setUI();
            }
            catch (Exception)
            {
                ShowAlert("Error in updating data. Please check network connectivity..!");
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserId.Text))
            {
                ShowAlert("Id should not be empty...!");
                return;
            }
            int id = Convert.ToInt32(UserId.Text);
            try
            {
                await DeleteUserDetails(id);
                setUI();
            }
            catch (Exception)
            {
                ShowAlert("Error in deleting data. Please check network connectivity..!");
            }
        }

        private void BtnGet_Clicked(object sender, EventArgs e)
        {
            setUI();
            DataRealTimeUserDetails();
        }

        private void ShowAlert(string message)
        {
            DisplayAlert("Alert", message, "OK");
        }

        private async void setUI()
        {
            try
            {
                collection.Clear();
                var list = await GetUsersDetails();
                for (int i = 0; i < list.Count; i++)
                {
                    collection.Add(list[i]);
                }
                UserRecordList.ItemsSource = collection;
            }
            catch (Exception)
            {
                ShowAlert("Error in fetching data. Please check network connectivity..!");
            }
        }

        public async Task<List<User>> GetUsersDetails()
        {
            var userList = await fbClient.Child(USERS).OnceAsync<User>();
            return userList.Select(item => new User
            {
                Id = item.Object.Id,
                Name = item.Object.Name
            }).ToList();
        }

        public async Task AddUsersDetails(int userId, string name)
        {
            await fbClient.Child(USERS)
                .PostAsync(new User() { Name = name, Id = userId });
        }

        public async Task UpdateUserDetails(int userId, string name)
        {
            var toUpdatePerson = (await fbClient.Child(USERS)
              .OnceAsync<User>()).Where(a => a.Object.Id == userId).FirstOrDefault();
            if (toUpdatePerson == null)
            {
                ShowAlert("Record doesnot exist");
                return;
            }
            await fbClient
              .Child(USERS)
              .Child(toUpdatePerson.Key)
              .PutAsync(new User() { Id = userId, Name = name });
        }

        public async Task DeleteUserDetails(int userId)
        {
            var toUpdatePerson = (await fbClient.Child(USERS)
              .OnceAsync<User>()).Where(a => a.Object.Id == userId).FirstOrDefault();
            if (toUpdatePerson == null)
            {
                ShowAlert("Record doesnot exist");
                return;
            }
            await fbClient
              .Child(USERS)
              .Child(toUpdatePerson.Key)
              .DeleteAsync();
        }

        public void DataRealTimeUserDetails()
        {
            var observable = fbClient
                                .Child(USERS)
                                .AsObservable<User>()
                                .Subscribe(eventReceived => DoSomething(eventReceived));
        }

        public void DoSomething(FirebaseEvent<User> fbEvent)
        {
            User user = fbEvent.Object;

            var obj = fbEvent.Key + " " + fbEvent.EventType
                + " ID:" + fbEvent.Object.Id + " Name:" + fbEvent.Object.Name;
            Console.WriteLine(obj);
            if (fbEvent.EventType == FirebaseEventType.InsertOrUpdate)
            {
                var found = collection.FirstOrDefault(i => i.Id == user.Id);

                if (found == null)
                {
                    //  is NOT in the observableCollection - add it
                    collection.Add(user);
                }
                else
                {
                    // event was updated 
                    int tempIndex = collection.IndexOf(found);
                    collection[tempIndex] = user;
                }
            }
            else if (fbEvent.EventType == FirebaseEventType.Delete)
            {
                collection.Remove(user);
            }
        }
    }
}