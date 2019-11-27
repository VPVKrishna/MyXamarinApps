using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FirebaseApp
{
    [Table("Employee")]
    class EmployeeInfo : INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id {
            set { _id = value; OnPropertyChanged(nameof(Id)); }
            get { return _id; }
        }

        private string _name;
        [NotNull]
        [MaxLength(20)]
        public string Name
        {
            set { _name = value; OnPropertyChanged(nameof(Name)); }
            get { return _name; }
        }

        private int _sal;
        public int Salary
        {
            set { _sal = value; OnPropertyChanged(nameof(Salary)); }
            get { return _sal; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
