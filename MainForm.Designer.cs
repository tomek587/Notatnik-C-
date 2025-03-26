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

        /// <summary>
        /// Wymagana metoda dla obsługi czyszczenia zasobów.
        /// </summary>
        /// <param name="disposing">True, jeśli zarządzane zasoby mają być usunięte; w przeciwnym razie False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lstNotatki = new System.Windows.Forms.ListBox();
            this.txtNotatka = new System.Windows.Forms.TextBox();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnUsun = new System.Windows.Forms.Button();
            this.btnWyloguj = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(12, 9);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(65, 15);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Witaj, user!";
            // 
            // lstNotatki
            // 
            this.lstNotatki.FormattingEnabled = true;
            this.lstNotatki.ItemHeight = 15;
            this.lstNotatki.Location = new System.Drawing.Point(15, 40);
            this.lstNotatki.Name = "lstNotatki";
            this.lstNotatki.Size = new System.Drawing.Size(300, 139);
            this.lstNotatki.TabIndex = 1;
            this.lstNotatki.SelectedIndexChanged += new System.EventHandler(this.lstNotatki_SelectedIndexChanged);
            // 
            // txtNotatka
            // 
            this.txtNotatka.Location = new System.Drawing.Point(15, 200);
            this.txtNotatka.Multiline = true;
            this.txtNotatka.Name = "txtNotatka";
            this.txtNotatka.Size = new System.Drawing.Size(300, 80);
            this.txtNotatka.TabIndex = 2;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(15, 300);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(75, 23);
            this.btnDodaj.TabIndex = 3;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnUsun
            // 
            this.btnUsun.Location = new System.Drawing.Point(115, 300);
            this.btnUsun.Name = "btnUsun";
            this.btnUsun.Size = new System.Drawing.Size(75, 23);
            this.btnUsun.TabIndex = 4;
            this.btnUsun.Text = "Usuń";
            this.btnUsun.UseVisualStyleBackColor = true;
            this.btnUsun.Click += new System.EventHandler(this.btnUsun_Click);
            // 
            // btnWyloguj
            // 
            this.btnWyloguj.Location = new System.Drawing.Point(220, 300);
            this.btnWyloguj.Name = "btnWyloguj";
            this.btnWyloguj.Size = new System.Drawing.Size(75, 23);
            this.btnWyloguj.TabIndex = 5;
            this.btnWyloguj.Text = "Wyloguj";
            this.btnWyloguj.UseVisualStyleBackColor = true;
            this.btnWyloguj.Click += new System.EventHandler(this.btnWyloguj_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(340, 350);
            this.Controls.Add(this.btnWyloguj);
            this.Controls.Add(this.btnUsun);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.txtNotatka);
            this.Controls.Add(this.lstNotatki);
            this.Controls.Add(this.lblWelcome);
            this.Name = "MainForm";
            this.Text = "Notatnik";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
