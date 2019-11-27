using System;
using System.Collections.Generic;
using System.Text;

namespace FirebaseApp
{
    public interface ISqliteConnection
    {
        SQLite.SQLiteConnection GetDbConnection();
    }
}
