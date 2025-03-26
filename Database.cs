using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Notatnik
{
    public class Database
    {
        private string connectionString = "Server=localhost;User ID=root;Password=;SslMode=none;";

        public Database()
        {
            CreateDatabaseIfNotExists();
        }

        private void CreateDatabaseIfNotExists()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmdCheckDatabase = new MySqlCommand("SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'notatnik'", conn);
                var result = cmdCheckDatabase.ExecuteScalar();

                if (result == null)
                {
                    MySqlCommand cmdCreateDatabase = new MySqlCommand("CREATE DATABASE notatnik", conn);
                    cmdCreateDatabase.ExecuteNonQuery();
                }
            }

            connectionString = "Server=localhost;Database=notatnik;User ID=root;Password=;SslMode=none;";
            CreateTablesIfNotExists();
        }

        private void CreateTablesIfNotExists()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS users (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        login VARCHAR(255) NOT NULL UNIQUE,
                        password VARCHAR(255) NOT NULL
                    )";
                MySqlCommand cmdCreateUsersTable = new MySqlCommand(createUsersTable, conn);
                cmdCreateUsersTable.ExecuteNonQuery();

                string createNotatkiTable = @"
                    CREATE TABLE IF NOT EXISTS notatki (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        tresc TEXT NOT NULL,
                        user_id INT,
                        FOREIGN KEY (user_id) REFERENCES users(id)
                    )";
                MySqlCommand cmdCreateNotatkiTable = new MySqlCommand(createNotatkiTable, conn);
                cmdCreateNotatkiTable.ExecuteNonQuery();
            }
        }

        public bool CheckUser(string login, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE login = @login AND password = SHA2(@password, 256)", conn);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);
                return cmd.ExecuteScalar() != null;
            }
        }

        public bool CheckUserExists(string login)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM users WHERE login = @login", conn);
                cmd.Parameters.AddWithValue("@login", login);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public void InsertUser(string login, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO users (login, password) VALUES (@login, SHA2(@password, 256))", conn);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable SelectNotatkiByUser(string login)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM notatki WHERE user_id = (SELECT id FROM users WHERE login = @login)", conn);
                cmd.Parameters.AddWithValue("@login", login);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public void InsertNotatka(string tresc, string login)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO notatki (tresc, user_id) VALUES (@tresc, (SELECT id FROM users WHERE login = @login))", conn);
                cmd.Parameters.AddWithValue("@tresc", tresc);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteNotatkaById(int noteId, string login)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand cmdDelete = new MySqlCommand("DELETE FROM notatki WHERE id = @id AND user_id = (SELECT id FROM users WHERE login = @login)", conn);
                cmdDelete.Parameters.AddWithValue("@id", noteId);
                cmdDelete.Parameters.AddWithValue("@login", login);
                int rowsAffected = cmdDelete.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("Nie znaleziono notatki do usunięcia!");
                }
            }
        }

        public DataTable SelectNotatkaById(int notatkaId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM notatki WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", notatkaId);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
