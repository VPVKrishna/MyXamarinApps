using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirebaseApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatabasePage : ContentPage
    {
        private SQLiteConnection connection;
        // Reference link: https://msdn.microsoft.com/en-us/magazine/mt736454.aspx
        public DatabasePage()
        {
            InitializeComponent();
            connection = DependencyService.Get<ISqliteConnection>().GetDbConnection();
            connection.CreateTable<EmployeeInfo>();
            if (!connection.Table<EmployeeInfo>().Any())
            {
                AddDefaultEmployee(new EmployeeInfo() { Id = 1, Name = "Test Employee", Salary = 2000, });
            }
        }

        private void AddDefaultEmployee(EmployeeInfo info)
        {
            connection.Insert(info);
        }

        private int GetEmployeeSize()
        {
            var employees = connection.Table<EmployeeInfo>();
            int size = employees.Count();
            return size;
        }

        private List<EmployeeInfo> GetEmployeeList()
        {
            var employees = connection.Table<EmployeeInfo>();

            return employees.ToList();
        }

        private void AddRecord_Clicked(object sender, EventArgs e)
        {
            var employee = new EmployeeInfo() { Id = Convert.ToInt32(UserId.Text), Name = UserName.Text };
            AddDefaultEmployee(employee);
        }

        private void UpdateRecord_Clicked(object sender, EventArgs e)
        {
            var info = new EmployeeInfo() { Id = Convert.ToInt32(UserId.Text), Name = UserName.Text };
            connection.Update(info);
        }

        private void GetRecord_Clicked(object sender, EventArgs e)
        {
            var employees = GetEmployeeList();
            EmployeeRecordList.ItemsSource = employees;
        }

        private void DeleteRecord_Clicked(object sender, EventArgs e)
        {
            var info = new EmployeeInfo() { Id = Convert.ToInt32(UserId.Text) };
            connection.Delete(info);
        }

        private void EmployeeRecordList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as EmployeeInfo;
            UserId.Text = Convert.ToString(item.Id);
            UserName.Text = item.Name;
        }

    }
}