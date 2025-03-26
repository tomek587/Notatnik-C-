using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notatnik
{
    public partial class LoginForm : Form
    {
        private Database db;

        public LoginForm()
        {
            InitializeComponent();
            db = new Database();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (login == "" || password == "")
            {
                MessageBox.Show("Podaj login i hasło", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (db.CheckUser(login, password))
            {
                OpenNotatnik(login);
            }
            else if (!db.CheckUserExists(login))
            {
                db.InsertUser(login, password);
                OpenNotatnik(login);
            }
            else
            {
                MessageBox.Show("Niepoprawne hasło!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenNotatnik(string login)
        {
            MainForm mainForm = new MainForm(login);
            this.Hide();
            mainForm.ShowDialog();
            this.Show();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
