using FirebaseApp.Helper;
using FirebaseApp.Model;
using FirebaseApp.Repository;
using FirebaseApp.View.Blocks;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace FirebaseApp.ViewModel
{
    public class BookViewModel : BaseNotifier
    {
        #region private variables
        private readonly IRepository repository;
        private ObservableCollection<BookModel> _collection = new ObservableCollection<BookModel>();
        private TextModelBlock _bookId;
        private TextModelBlock _bookName;
        private TextModelBlock _bookAuthor;
        private TextModelBlock _bookPublisher;
        private TextModelBlock _bookPublishedOn;
        private string _pickerItem;
        #endregion

        #region public variables
        public ObservableCollection<BookModel> BookModelCollection
        {
            private set
            {
                SetPropertyValue(ref _collection, value);
            }
            get { return _collection; }
        }
        public TextModelBlock BookIdBlock
        {
            set
            {
                SetPropertyValue(ref _bookId, value);
            }
            get
            {
                return _bookId;
            }
        }
        public TextModelBlock BookNameBlock
        {
            set
            {
                SetPropertyValue(ref _bookName, value);
            }
            get
            {
                return _bookName;
            }
        }
        public TextModelBlock BookAuthorBlock
        {
            set
            {
                SetPropertyValue(ref _bookAuthor, value);
            }
            get
            {
                return _bookAuthor;
            }
        }
        public TextModelBlock BookPublisherBlock
        {
            set
            {
                SetPropertyValue(ref _bookPublisher, value);
            }
            get
            {
                return _bookPublisher;
            }
        }
        public TextModelBlock BookPublishedOnBlock
        {
            set
            {
                SetPropertyValue(ref _bookPublishedOn, value);
            }
            get
            {
                return _bookPublishedOn;
            }
        }
        public TextModelBlock SubmitBlock { set; get; }
        public string SelectedPickerItem {
            set
            {
                SetPropertyValue(ref _pickerItem, value);
            }
            get
            {
                return _pickerItem;
            }
        }

        public ICommand PerformSubmitOperation { private set; get; }
        public ICommand OperationPickerItemSelectionChanged { private set; get; }

        #endregion

        public BookViewModel(IRepository repository)
        {
            this.repository = repository;
            repository.GetBooks((bookList) =>
            {
                BookModelCollection = new ObservableCollection<BookModel>(bookList);
                SelectedPickerItem = "Add Book";
                InitializeBlocks();
                SetUIBlocks();
            });
            PerformSubmitOperation = new Command(() => {
                PerforOperation();
            });

            OperationPickerItemSelectionChanged = new Command((sender) =>
            {
                var myPicker = sender as Picker;
                var opr = SelectedPickerItem;
                SetUIBlocks();
            });
        }

        //private void SetPickerOperationSelection()
        //{
        //    var picker = (Picker)sender;
        //    int selectedIndex = picker.SelectedIndex;

        //    if (selectedIndex != -1)
        //    {
        //        var operation = picker.Items[selectedIndex];
        //        _viewModel.SelectedPickerItem = operation;
        //        _viewModel.SetUIBlocks();
        //    }
        //}

        private void InitializeBlocks()
        {
            BookIdBlock = new TextModelBlock() {
                Text = "",
                Hint = "Please enter Id",
                IsVisible = true,
                IsEnabled = true,
            };
            BookNameBlock = new TextModelBlock() {
                Text = "",
                Hint = "Please enter name",
                IsVisible = true,
                IsEnabled = true,
            };
            BookAuthorBlock = new TextModelBlock() {
                Text = "",
                Hint = "Please enter author name",
                IsVisible = true,
                IsEnabled = true,
            };
            BookPublisherBlock = new TextModelBlock() {
                Text = "",
                Hint = "Please enter publisher name",
                IsVisible = true,
                IsEnabled = true,
            };
            BookPublishedOnBlock = new TextModelBlock() {
                Text = "",
                Hint = "Please enter published date",
                IsVisible = false,
                IsEnabled = true,
            };
        }

        public void AddBlocksSetup()
        {
            BookIdBlock.IsEnabled = false;
            BookNameBlock.IsEnabled = true;
            BookAuthorBlock.IsEnabled = true;
            BookPublisherBlock.IsEnabled = true;
            BookPublishedOnBlock.IsEnabled = true;

            BookIdBlock.IsVisible = true;
            BookNameBlock.IsVisible = true;
            BookAuthorBlock.IsVisible = true;
            BookPublisherBlock.IsVisible = true;
            BookPublishedOnBlock.IsVisible = true;                       
        }

        public void GetBlocksSetup()
        {
            BookIdBlock.IsEnabled = true;
            BookNameBlock.IsEnabled = false;
            BookAuthorBlock.IsEnabled = false;
            BookPublisherBlock.IsEnabled = false;
            BookPublishedOnBlock.IsEnabled = false;

            BookIdBlock.IsVisible = true;
            BookNameBlock.IsVisible = true;
            BookAuthorBlock.IsVisible = true;
            BookPublisherBlock.IsVisible = true;
            BookPublishedOnBlock.IsVisible = true;
        }

        public void UpdateBlocksSetup()
        {
            BookIdBlock.IsEnabled = true;
            BookNameBlock.IsEnabled = true;
            BookAuthorBlock.IsEnabled = true;
            BookPublisherBlock.IsEnabled = true;
            BookPublishedOnBlock.IsEnabled = true;

            BookIdBlock.IsVisible = true;
            BookNameBlock.IsVisible = true;
            BookAuthorBlock.IsVisible = true;
            BookPublisherBlock.IsVisible = true;
            BookPublishedOnBlock.IsVisible = true;
        }

        public void DeleteBlocksSetup()
        {
            BookIdBlock.IsEnabled = true;
            BookNameBlock.IsEnabled = false;
            BookAuthorBlock.IsEnabled = false;
            BookPublisherBlock.IsEnabled = false;
            BookPublishedOnBlock.IsEnabled = false;

            BookIdBlock.IsVisible = true;
            BookNameBlock.IsVisible = true;
            BookAuthorBlock.IsVisible = true;
            BookPublisherBlock.IsVisible = true;
            BookPublishedOnBlock.IsVisible = true;
        }


        public void Insert(BookModel bookModel)
        {
            repository.AddBook(bookModel, () => 
            {
                BookModelCollection.Add(bookModel);
            });
        }

        public void Update(BookModel bookModel)
        {
            repository.UpdateBook(bookModel, () =>
            {
                BookModel model = BookModelCollection.Where(x => x.Id == bookModel.Id).FirstOrDefault();
                if (model != null)
                {
                    BookModel.CopyBookData(bookModel, model);
                }
            });
        }

        public void Delete(BookModel bookModel)
        {
            repository.DeleteBook(bookModel.Id, (isDeleted) =>
            {
                bool isDeletedFromRepo = isDeleted;
                BookModelCollection.Remove(bookModel);
            });
        }

        public void Delete(int bookId)
        {
            repository.DeleteBook(bookId, (isDeleted) =>
            {
                bool isDeletedFromRepo = isDeleted;

                BookModel bookModel = BookModelCollection.Where(x => x.Id == bookId).FirstOrDefault();
                bool isDeletedFromList = BookModelCollection.Remove(bookModel);
            });
           
        }

        public void GetAll()
        {
            repository.GetBooks((bookList) =>
            {
                BookModelCollection = new ObservableCollection<BookModel>(bookList);
            });            
        }

        public void PerforOperation()
        {
            switch(SelectedPickerItem)
            {
                case "":
                case BookOptions.ADD_BOOK:
                    BookModel bookModel = GetBookModelFromBlocks();
                    bookModel.Id = GetNextRowId();
                    Insert(bookModel);
                    PopulateBookModelToViews(new BookModel());
                    SetDefaultBookId();
                    break;
                case BookOptions.UPDATE_BOOK:
                    bookModel = GetBookModelFromBlocks();
                    Update(bookModel);
                    PopulateBookModelToViews(new BookModel());
                    break;
                case BookOptions.DELETE_BOOK:
                    string idText = BookIdBlock.Text;
                    int id = Convert.ToInt32(idText);
                    Delete(id);
                    break;
                case BookOptions.GET_BOOK:
                    idText = BookIdBlock.Text;
                    id = Convert.ToInt32(idText);
                    repository.GetBook(id, (model) => 
                    {
                        if (model != null)
                        {
                            PopulateBookModelToViews(model);
                        }
                        else
                        {
                            PopulateBookModelToViews(new BookModel());
                        }
                    });
                    break;
                case BookOptions.GET_LIST_BOOKS:
                    GetAll();
                    break;
            }
        }

        public void SetUIBlocks()
        {
            // Resetting or Clearing the views
            PopulateBookModelToViews(new BookModel());

            switch (SelectedPickerItem)
            {
                case "":
                case BookOptions.ADD_BOOK:
                    AddBlocksSetup();
                    SetDefaultBookId();
                    break;
                case BookOptions.UPDATE_BOOK:
                    UpdateBlocksSetup();
                    break;
                case BookOptions.DELETE_BOOK:
                    DeleteBlocksSetup();
                    break;
                case BookOptions.GET_BOOK:
                    GetBlocksSetup();
                    break;
                case BookOptions.GET_LIST_BOOKS:
                    break;
            }

        }

        private BookModel GetBookModelFromBlocks()
        {
            BookModel model = new BookModel();
            string idText = BookIdBlock.Text;
            if (!string.IsNullOrEmpty(idText))
            {
                model.Id = Convert.ToInt32(idText);
            }
            model.Name = BookNameBlock.Text;
            model.Author = BookAuthorBlock.Text;
            model.Publisher = BookPublisherBlock.Text;
            model.PublishedOn = BookPublisherBlock.Text;
            return model;
        }

        private void PopulateBookModelToViews(BookModel model)
        {
            BookIdBlock.Text = Convert.ToString(model.Id);
            BookNameBlock.Text = model.Name;
            BookAuthorBlock.Text = model.Author;
            BookPublisherBlock.Text = model.Publisher;
            BookPublishedOnBlock.Text = model.PublishedOn;
        }

        private int GetNextRowId()
        {
            BookModel bookModel = BookModelCollection.LastOrDefault();
            if (bookModel != null)
            {
                return bookModel.Id + 1;
            }
            return 1;
        }

        private void SetDefaultBookId()
        {
            BookIdBlock.Text = Convert.ToString(GetNextRowId());
        }
    }
}
