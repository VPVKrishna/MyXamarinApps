using System;
using System.Collections.Generic;
using System.Linq;
using FirebaseApp.Model;
using Xamarin.Essentials;

namespace FirebaseApp.Repository
{
    public class PrefRepository : IRepository
    {

        //Refer below links
        // https://docs.microsoft.com/en-us/xamarin/essentials/preferences?tabs=android
        // https://docs.microsoft.com/en-us/xamarin/essentials/secure-storage?context=xamarin%2Fandroid&tabs=android
        // https://docs.microsoft.com/en-us/xamarin/essentials/get-started?tabs=windows%2Candroid

        private const string PREF_LIST = "List";
        private List<BookModel> BookModelList;

        public PrefRepository()
        {
            if (!Xamarin.Forms.Application.Current.Properties.ContainsKey(PREF_LIST))
            {
                Xamarin.Forms.Application.Current.Properties[PREF_LIST] = new List<BookModel>();
                Xamarin.Forms.Application.Current.SavePropertiesAsync();
            }
            BookModelList = Xamarin.Forms.Application.Current.Properties[PREF_LIST] as List<BookModel>;
        }

        public void AddBook(BookModel bookModel, Action AddAction)
        {
            BookModelList.Add(bookModel);
            NotifyBookData();
            AddAction?.Invoke();
        }

        public void DeleteBook(int bookId, Action<bool> DeleteAction)
        {
            int deletedRecords = BookModelList.RemoveAll(x => x.Id == bookId);
            NotifyBookData();
            bool isDeleted = deletedRecords != 0;
            DeleteAction?.Invoke(isDeleted);
        }

        public void GetBooks(Action<List<BookModel>> GetListAction)
        {
            List<BookModel> list = Xamarin.Forms.Application.Current.Properties[PREF_LIST] as List<BookModel>;
            if (list == null)
            {
                list = new List<BookModel>();
            }
            GetListAction?.Invoke(list);
        }

        public void UpdateBook(BookModel bookModel, Action updateAction)
        {
            BookModel model = BookModelList.Where(x => x.Id == bookModel.Id).FirstOrDefault();
            if (model != null)
            {
                BookModel.CopyBookData(bookModel, model);
                NotifyBookData();
            }
            updateAction?.Invoke();
        }

        public void GetBook(int bookId, Action<BookModel> GetAction)
        {
            BookModel bookModel = BookModelList.Where(x => x.Id == bookId).FirstOrDefault();
            GetAction?.Invoke(bookModel);
        }

        private void NotifyBookData()
        {
            Xamarin.Forms.Application.Current.Properties[PREF_LIST] = BookModelList;
            Xamarin.Forms.Application.Current.SavePropertiesAsync();
        }
    }
}
