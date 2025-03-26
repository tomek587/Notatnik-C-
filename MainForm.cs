using System;
using System.Data;
using System.Windows.Forms;

namespace Notatnik
{
    public partial class MainForm : Form
    {
        private Database db;
        private string login;

        public MainForm(string userLogin)
        {
            InitializeComponent();
            db = new Database();
            login = userLogin;
            lblWelcome.Text = $"Witaj, {login}!";
            LoadNotatki();
        }

        // Funkcja ładująca notatki z bazy
        private void LoadNotatki(string search = "")
        {
            lstNotatki.Items.Clear();
            DataTable notatki = db.SelectNotatkiByUser(login);

            foreach (DataRow row in notatki.Rows)
            {
                string content = row["tresc"].ToString();
                string displayText = content.Length > 30 ? content.Substring(0, 30) + "..." : content;
                lstNotatki.Items.Add(displayText);
            }

            if (lstNotatki.Items.Count == 0)
                lstNotatki.Items.Add("Brak notatek");
        }

        // Funkcja dodająca nową notatkę
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            string text = txtNotatka.Text.Trim();
            if (text != "")
            {
                db.InsertNotatka(text, login);
                txtNotatka.Clear();
                LoadNotatki();
            }
            else
            {
                MessageBox.Show("Notatka nie może być pusta!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Funkcja usuwająca zaznaczoną notatkę
        private void btnUsun_Click(object sender, EventArgs e)
        {
            if (lstNotatki.SelectedIndex == -1 || lstNotatki.SelectedItem.ToString() == "Brak notatek")
                return;

            int selectedIndex = lstNotatki.SelectedIndex;
            db.DeleteNotatka(selectedIndex, login);
            txtNotatka.Clear();
            LoadNotatki();
        }

        // Funkcja wylogowująca użytkownika
        private void btnWyloguj_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Funkcja uruchamiana przy wczytaniu formularza (na razie pusta)
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
