using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Notatnik
{
    public class Database
    {
        // Połączenie z bazą danych
        string connectionString = "Server=localhost;Database=notatnik;User ID=root;Password=;SslMode=none;";

        // Sprawdzenie, czy użytkownik istnieje
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

        // Sprawdzenie, czy użytkownik już istnieje
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

        // Dodanie nowego użytkownika
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

        // Pobranie notatek dla danego użytkownika
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

        // Dodanie nowej notatki
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

        // Usunięcie notatki
        public void DeleteNotatka(int index, string login)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Pobranie ID notatki, którą chcemy usunąć
                MySqlCommand cmdSelect = new MySqlCommand(
                    "SELECT id FROM notatki WHERE user_id = (SELECT id FROM users WHERE login = @login) LIMIT 1 OFFSET @index", conn);
                cmdSelect.Parameters.AddWithValue("@login", login);
                cmdSelect.Parameters.AddWithValue("@index", index);

                object result = cmdSelect.ExecuteScalar();

                if (result != null)
                {
                    int notatkaId = Convert.ToInt32(result);

                    // Usunięcie notatki o pobranym ID
                    MySqlCommand cmdDelete = new MySqlCommand("DELETE FROM notatki WHERE id = @id", conn);
                    cmdDelete.Parameters.AddWithValue("@id", notatkaId);
                    cmdDelete.ExecuteNonQuery();
                }
                else
                {
                    // Jeżeli nie znaleziono notatki
                    throw new Exception("Nie znaleziono notatki do usunięcia!");
                }
            }
        }
    }
}
