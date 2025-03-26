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

        private void btnUsun_Click(object sender, EventArgs e)
        {
            if (lstNotatki.SelectedIndex == -1 || lstNotatki.SelectedItem.ToString() == "Brak notatek")
                return;

            string selectedNoteText = lstNotatki.SelectedItem.ToString();

            int noteId = GetNoteIdByText(selectedNoteText);

            if (noteId != -1)
            {
                try
                {
                    db.DeleteNotatkaById(noteId, login);
                    LoadNotatki();
                    txtNotatka.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas usuwania notatki: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nie znaleziono notatki do usunięcia!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWyloguj_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadNotatki();
        }

        private int GetNoteIdByText(string noteText)
        {
            DataTable notes = db.SelectNotatkiByUser(login);
            foreach (DataRow row in notes.Rows)
            {
                string content = row["tresc"].ToString();
                string displayText = content.Length > 30 ? content.Substring(0, 30) + "..." : content;
                if (displayText == noteText)
                {
                    return Convert.ToInt32(row["id"]);
                }
            }
            return -1;
        }

        private void lstNotatki_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNotatki.SelectedIndex != -1 && lstNotatki.SelectedItem.ToString() != "Brak notatek")
            {
                string selectedNoteText = lstNotatki.SelectedItem.ToString();

                string fullNoteText = GetFullNoteTextByDisplayText(selectedNoteText);

                txtNotatka.Text = fullNoteText;
            }
        }

        private string GetFullNoteTextByDisplayText(string displayText)
        {
            DataTable notes = db.SelectNotatkiByUser(login);
            foreach (DataRow row in notes.Rows)
            {
                string content = row["tresc"].ToString();
                string displayNoteText = content.Length > 30 ? content.Substring(0, 30) + "..." : content;

                if (displayNoteText == displayText)
                {
                    return content;
                }
            }
            return string.Empty;
        }
    }
}
