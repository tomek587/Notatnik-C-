namespace Notatnik
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.ListBox lstNotatki;
        private System.Windows.Forms.TextBox txtNotatka;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnUsun;
        private System.Windows.Forms.Button btnWyloguj;

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            lstNotatki = new ListBox();
            txtNotatka = new TextBox();
            btnDodaj = new Button();
            btnUsun = new Button();
            btnWyloguj = new Button();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(12, 9);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(65, 15);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Witaj, user!";
            // 
            // lstNotatki
            // 
            lstNotatki.FormattingEnabled = true;
            lstNotatki.ItemHeight = 15;
            lstNotatki.Location = new Point(15, 40);
            lstNotatki.Name = "lstNotatki";
            lstNotatki.Size = new Size(300, 139);
            lstNotatki.TabIndex = 1;
            // 
            // txtNotatka
            // 
            txtNotatka.Location = new Point(15, 200);
            txtNotatka.Multiline = true;
            txtNotatka.Name = "txtNotatka";
            txtNotatka.Size = new Size(300, 80);
            txtNotatka.TabIndex = 2;
            // 
            // btnDodaj
            // 
            btnDodaj.Location = new Point(15, 300);
            btnDodaj.Name = "btnDodaj";
            btnDodaj.Size = new Size(75, 23);
            btnDodaj.TabIndex = 3;
            btnDodaj.Text = "Dodaj";
            btnDodaj.UseVisualStyleBackColor = true;
            btnDodaj.Click += btnDodaj_Click;
            // 
            // btnUsun
            // 
            btnUsun.Location = new Point(115, 300);
            btnUsun.Name = "btnUsun";
            btnUsun.Size = new Size(75, 23);
            btnUsun.TabIndex = 4;
            btnUsun.Text = "Usuń";
            btnUsun.UseVisualStyleBackColor = true;
            btnUsun.Click += btnUsun_Click;
            // 
            // btnWyloguj
            // 
            btnWyloguj.Location = new Point(220, 300);
            btnWyloguj.Name = "btnWyloguj";
            btnWyloguj.Size = new Size(75, 23);
            btnWyloguj.TabIndex = 5;
            btnWyloguj.Text = "Wyloguj";
            btnWyloguj.UseVisualStyleBackColor = true;
            btnWyloguj.Click += btnWyloguj_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(340, 350);
            Controls.Add(btnWyloguj);
            Controls.Add(btnUsun);
            Controls.Add(btnDodaj);
            Controls.Add(txtNotatka);
            Controls.Add(lstNotatki);
            Controls.Add(lblWelcome);
            Name = "MainForm";
            Text = "Notatnik";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
