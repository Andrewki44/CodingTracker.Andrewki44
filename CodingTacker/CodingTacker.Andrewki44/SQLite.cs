using System.Configuration;
using Microsoft.Data.Sqlite;
using Dapper;

namespace CodingTacker.Andrewki44
{
    static class SQLite
    {
        private static string dbFile { 
            get 
            {
                string? dbPath = ConfigurationManager.AppSettings.Get("Data Source");
                if (dbPath != null)
                    return Environment.CurrentDirectory + dbPath;
                else
                    return Environment.CurrentDirectory + "\\CodingDefault.db";
             }
        }
        private static string foreignKeys
        {
            get
            {
                string? foreignKeys = ConfigurationManager.AppSettings.Get("Foreign Keys");
                if (foreignKeys != null)
                    return foreignKeys;
                else
                    return "false";

            }
        }

        public static void SaveCodingSession(CodingSession session)
        {
            if (!File.Exists(dbFile))
                CreateDatabase();

            string commandText = @"
                INSERT INTO CodingSession (Start, End, Duration)
                VALUES (@sessionStart, @sessionEnd, @duration)
            ;";

            using (SqliteConnection conn = DbConnection())
            {
                conn.Execute(commandText, session);
            }
        }

        private static SqliteConnection DbConnection()
        {
            return new SqliteConnection("Data Source = " + dbFile + "; Foreign Keys = " + foreignKeys);
        }

        private static void CreateDatabase()
        {
            using (SqliteConnection conn = DbConnection())
            {
                conn.Open();
                conn.Execute(@"
                    CREATE TABLE CodingSession (
                        ID          INTEGER     PRIMARY KEY,
                        Start       TEXT        NOT NULL,
                        End         TEXT        NOT NULL,
                        Duration    TEXT        NOT NULL
                    );");
            }
        }

    }
}
