using System.Data.SQLite;

namespace Template.Infrastructure.Persistence;

public static class SQLiteExtensions
{
    public static void ConfigureDB(string dbFileName, string connectionString)
    {
        if (File.Exists(dbFileName)) return;
        
        SQLiteConnection.CreateFile(dbFileName);

        using (SQLiteConnection c = new SQLiteConnection(connectionString))
        {
            c.Open();

            var sql = @"
                CREATE TABLE COMPANIES (
                    COMP_ID BINARY(32) PRIMARY KEY,
                    COMP_CREATED_AT DATETIME,
                    COMP_UPDATED_AT DATETIME,
                    COMP_DELETED_AT DATETIME,
                    COMP_NAME VARCHAR(100),
                    COMP_EMAIL VARCHAR(100)
                );
            ";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
            {
                cmd.ExecuteNonQuery();
            }

            sql = @"
                CREATE TABLE SUBSIDIARIES (
                    SUBS_ID BINARY(32) PRIMARY KEY,
                    SUBS_CREATED_AT DATETIME,
                    SUBS_UPDATED_AT DATETIME,
                    SUBS_DELETED_AT DATETIME,
                    SUBS_NAME VARCHAR(100),
                    SUBS_ZIPCODE VARCHAR(100),
                    SUBS_STATE VARCHAR(100),
                    SUBS_CITY VARCHAR(100),
                    SUBS_NEIGHBORHOOD VARCHAR(100),
                    SUBS_STREET VARCHAR(100),
                    SUBS_NUMBER VARCHAR(100),
                    SUBS_COMPLEMENT VARCHAR(100),
                    SUBS_COMP_ID BINARY(32),
                    FOREIGN KEY(SUBS_COMP_ID) REFERENCES COMPANIES(COMP_ID)
                );
            ";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
            {
                cmd.ExecuteNonQuery();
            }

            c.Close();
        }
    }
}
