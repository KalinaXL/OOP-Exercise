using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using System.IO;

namespace DataOfUser
{
    public class MyDatabase : SQLiteOpenHelper
    {
        static string DATABASE_NAME = "Questions.db";
        static string TABLE_NAME = "Category";


        public MyDatabase(Context context, string name, SQLiteDatabase.ICursorFactory factory, int version) : base(context, DATABASE_NAME, null, 1)
        {
        }
        public void QueryData(string query)
        {
            SQLiteDatabase db = WritableDatabase;
            db.ExecSQL(query);
        }
        public ICursor GetData(string query)
        {
            SQLiteDatabase db = ReadableDatabase;
            return db.RawQuery(query, null);
        }
        public override void OnCreate(SQLiteDatabase db)
        {
            // db.ExecSQL("CREATE TABLE " + TABLE_NAME + "(ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, IsMidTerm INTEGER, Name TEXT)");
            string path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), "database.db");
            db = SQLiteDatabase.OpenDatabase("database.db", null, DatabaseOpenFlags.CreateIfNecessary);
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {

        }
    }
}