using FirebaseApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirebaseApp.Repository
{
    public class BookRepository : IRepository
    {
        private List<BookModel> BookModelList = new List<BookModel>();

        public void AddBook(BookModel bookModel, Action AddAction)
        {
            BookModelList.Add(bookModel);
            AddAction?.Invoke();
        }

        public void DeleteBook(int bookId, Action<bool> DeleteAction)
        {
            int deletedRecords = BookModelList.RemoveAll(x => x.Id == bookId);
            bool isDeleted = deletedRecords != 0;
            DeleteAction?.Invoke(isDeleted);
        }

        public void GetBooks(Action<List<BookModel>> GetListAction)
        {
            GetListAction?.Invoke(BookModelList);
        }

        public void UpdateBook(BookModel bookModel, Action updateAction)
        {
            BookModel model = BookModelList.Where(x => x.Id == bookModel.Id).FirstOrDefault();
            if (model != null)
            {
                BookModel.CopyBookData(bookModel, model);
            }
            updateAction?.Invoke();            
        }

        public void GetBook(int bookId, Action<BookModel> GetAction)
        {
            BookModel bookModel = BookModelList.Where(x => x.Id == bookId).FirstOrDefault();
            GetAction?.Invoke(bookModel);
        }
    }
}
