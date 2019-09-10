using Android.Database.Sqlite;
using Android.Content;

namespace DataOfUser
{
    public class MyDatabase : SQLiteOpenHelper
    {
        static string DATABASE_NAME = "QUESTIONS";
        SQLiteDatabase db;

        public MyDatabase(Context context, string name, SQLiteDatabase.ICursorFactory factory, int version) : base(context, name, factory, version)
        {
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            throw new System.NotImplementedException();
        }
    }
}