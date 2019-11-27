using FirebaseApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirebaseApp.Repository
{
    public interface IRepository
    {
        void AddBook(BookModel bookModel, Action AddAction = null);

        void UpdateBook(BookModel bookModel, Action UpdateAction = null);

        /// <summary>
        /// Used to delete the book from the list. if removed successfully returns true otherwise false;
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        void DeleteBook(int bookId, Action<bool> DeleteAction = null);

        void GetBooks(Action<List<BookModel>> GetListAction = null);

        void GetBook(int bookId, Action<BookModel> GetAction = null);
    }
}
