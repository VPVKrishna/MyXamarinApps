using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using FirebaseApp.Model;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace FirebaseApp.Repository
{
    public class FirebaseRepository : IRepository
    {
        // Reference link: https://www.c-sharpcorner.com/article/xamarin-forms-working-with-firebase-realtime-database-crud-operations/
        private FirebaseClient fbClient;
        private const string BOOK_STORE = "BookStore";

        public FirebaseRepository()
        {
            fbClient = new FirebaseClient("https://fir-app-b1cb2.firebaseio.com/");
        }

        public void AddBook(BookModel bookModel, Action AddAction)
        {
            AddBookAsync(bookModel).ContinueWith(task => {
                AddAction?.Invoke();
            });
        }

        private async Task AddBookAsync(BookModel bookModel)
        {
             await fbClient.Child(BOOK_STORE)
                .PostAsync(bookModel);
        }

        public void DeleteBook(int bookId, Action<bool> DeleteAction)
        {
            DeleteBookAsync(bookId).ContinueWith(task => {
                DeleteAction?.Invoke(task.Result);
            });
        }

        private async Task<bool> DeleteBookAsync(int bookId)
        {
            var fbBookModel = await GetFirebaseBook(bookId);
            if (fbBookModel == null)
            {
                //ShowAlert("Record doesnot exist");
                return false;
            }
            await fbClient
              .Child(BOOK_STORE)
              .Child(fbBookModel.Key)
              .DeleteAsync();
            return true;
        }

        public void GetBook(int bookId, Action<BookModel> GetAction)
        {
            GetBookAsync(bookId).ContinueWith(task => {
                GetAction?.Invoke(task.Result);
            });
        }

        private async Task<BookModel> GetBookAsync(int bookId)
        {
            var fbBookModel = await GetFirebaseBook(bookId);
            return fbBookModel.Object;
        }

        private async Task<FirebaseObject<BookModel>> GetFirebaseBook(int bookId)
        {
            var firebaseObjectBookModel = (await fbClient.Child(BOOK_STORE)
              .OnceAsync<BookModel>()).Where(a => a.Object.Id == bookId).FirstOrDefault();
            return firebaseObjectBookModel;
        }

        public void GetBooks(Action<List<BookModel>> GetListAction)
        {
            GetBooksAsync().ContinueWith(task => {
                GetListAction?.Invoke(task.Result);
            });
        }

        private async Task<List<BookModel>> GetBooksAsync()
        {
            var userList = await fbClient.Child(BOOK_STORE).OnceAsync<BookModel>();
            return userList.Select(item => new BookModel
            {
                Id = item.Object.Id,
                Name = item.Object.Name,
                Author = item.Object.Author,
                Publisher = item.Object.Publisher,
                PublishedOn = item.Object.PublishedOn
            }).ToList();
        }

        public void UpdateBook(BookModel bookModel, Action UpdateAction)
        {
            UpdateBookAsync(bookModel).ContinueWith(task => {
                UpdateAction?.Invoke();
            });
        }
        private async Task UpdateBookAsync(BookModel bookModel)
        {
            var fbBookModel = await GetFirebaseBook(bookModel.Id);
            if (fbBookModel == null)
            {
                //ShowAlert("Record doesnot exist");
                return;
            }
            await fbClient
              .Child(BOOK_STORE)
              .Child(fbBookModel.Key)
              .PutAsync(bookModel);
        }
    }
}
