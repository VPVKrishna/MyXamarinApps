using FirebaseApp.View.Blocks;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirebaseApp.Model
{
    public class BookModel : BaseNotifier
    {
        [PrimaryKey, AutoIncrement]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Author { set; get; }
        public string Publisher { set; get; }
        public string PublishedOn { set; get; }

        public static void CopyBookData(BookModel sourceModel, BookModel targetModel)
        {
            targetModel.Id = sourceModel.Id;
            targetModel.Name = sourceModel.Name;
            targetModel.Author = sourceModel.Author;
            targetModel.Publisher = sourceModel.Publisher;
            targetModel.PublishedOn = sourceModel.PublishedOn;
        }

        public override bool Equals(object obj)
        {
            var model = obj as BookModel;
            return model != null &&
                   Id == model.Id &&
                   Name == model.Name &&
                   Author == model.Author &&
                   Publisher == model.Publisher &&
                   PublishedOn == model.PublishedOn;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
