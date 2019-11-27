using System.IO;
using FirebaseApp.Droid;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidSqliteConnection))]
namespace FirebaseApp.Droid
{
    class AndroidSqliteConnection : ISqliteConnection
    {
        public SQLiteConnection GetDbConnection()
        {
            var dbName = "CustomersDb.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }
}