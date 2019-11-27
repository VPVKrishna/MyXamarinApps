using FirebaseApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FirebaseApp.Repository
{
    public class SqliteRepository : IRepository
    {
        private SQLiteConnection connection;

        public SqliteRepository()
        {
            connection = DependencyService.Get<ISqliteConnection>().GetDbConnection();
            connection.CreateTable<BookModel>();
        }

        public void AddBook(BookModel bookModel, Action AddAction)
        {
            int addedRecords = connection.Insert(bookModel);
            AddAction?.Invoke();
        }

        public void DeleteBook(int bookId, Action<bool> DeleteAction)
        {
            int deletedRecords = connection.Delete<BookModel>(bookId);
            bool isDeleted = deletedRecords != 0;
            DeleteAction?.Invoke(isDeleted);
        }

        public void GetBooks(Action<List<BookModel>> GetListAction)
        {
            List<BookModel> bookModels = connection.Table<BookModel>().ToList();
            GetListAction?.Invoke(bookModels);
        }

        public void UpdateBook(BookModel bookModel, Action updateAction)
        {
            int updatedRecords = connection.Update(bookModel);
            updateAction?.Invoke();
        }

        public void GetBook(int bookId, Action<BookModel> GetAction)
        {
            BookModel bookModel = connection.Get<BookModel>(bookId);
            GetAction?.Invoke(bookModel);
        }
    }
}
