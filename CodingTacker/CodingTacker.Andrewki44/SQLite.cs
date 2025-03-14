﻿using Microsoft.Data.Sqlite;
using Dapper;

namespace CodingTacker.Andrewki44
{
    static class SQLite
    {
        private static string DbFile { 
            get 
            {
                string? dbPath = System.Configuration.ConfigurationManager.AppSettings.Get("Data Source");
                if (dbPath != null)
                    return Environment.CurrentDirectory + dbPath;
                else
                    return Environment.CurrentDirectory + "\\CodingDefault.db";
             }
        }
        private static string ForeignKeys
        {
            get
            {
                string? foreignKeys = System.Configuration.ConfigurationManager.AppSettings.Get("Foreign Keys");
                if (foreignKeys != null)
                    return foreignKeys;
                else
                    return "false";

            }
        }

        public static void InsertCodingSession(CodingSession session)
        {
            if (!File.Exists(DbFile))
                CreateDatabase();

            string commandText = @"
                INSERT INTO CodingSession (Start, End, Duration)
                VALUES (@sessionStart, @sessionEnd, @seconds)
            ;";

            using (SqliteConnection conn = DbConnection())
            {
                conn.Execute(commandText, session);
            }
        }

        public static void UpdateCodingSession(CodingSession log, CodingSession session)
        {
            if (!File.Exists(DbFile))
                CreateDatabase();

            string commandText = @"
                UPDATE CodingSession
                SET Start = @sessionStart,
                    End = @sessionEnd,
                    Duration = @seconds
                WHERE ID = @ID
            ;";

            CodingSession updateSession = new CodingSession();
            
            if (session.SessionStart.HasValue && session.SessionEnd.HasValue)
            {
                updateSession = new CodingSession(session.SessionStart.Value, session.SessionEnd.Value);
                updateSession.ID = log.ID;
            }

            using (SqliteConnection conn = DbConnection())
            {
                conn.Execute(commandText, updateSession);
            }
        }

        public static void DeleteCodingSession(CodingSession session)
        {
            if (!File.Exists(DbFile))
                CreateDatabase();

            string commandText = @"
                DELETE FROM CodingSession
                WHERE ID = @ID
            ;";

            using (SqliteConnection conn = DbConnection())
            {
                conn.Execute(commandText, session);
            }
        }

        public static List<CodingSession> GetCodingSessions()
        {
            if (!File.Exists(DbFile))
                CreateDatabase();

            string commandText = @"
                SELECT ID AS ID, Start AS sessionStart, End AS sessionEnd, Duration AS Seconds
                FROM CodingSession
                ORDER BY ID
            ;";

            using (SqliteConnection conn = DbConnection())
            {
                return conn.Query<CodingSession>(commandText).ToList();
            }
        }

        private static SqliteConnection DbConnection()
        {
            return new SqliteConnection("Data Source = " + DbFile + "; Foreign Keys = " + ForeignKeys);
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
                        Duration    INTEGER     NOT NULL
                    );");
            }
        }

    }
}
