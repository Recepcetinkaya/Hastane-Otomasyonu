using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmSekreterGiris : Form, IFormComponent
    {
        private NotificationManager notificationManager;

        public FrmSekreterGiris(NotificationManager notificationManager)
        {
            InitializeComponent();
            this.notificationManager = notificationManager;
        }

        // Eski constructor: Yeni constructor'ı çağırır
        public FrmSekreterGiris() : this(null) { }

        public string FormName
        {
            get { return "FrmSekreterGiris"; }
        }

        public Form GetForm()
        {
            return this;
        }

        public void Display()
        {
            this.Show();
        }

        private sqlbaglantisi bgl = sqlbaglantisi.GetInstance();

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            var loginContext = new LoginContext(new SekreterLoginStrategy());
            bool isSuccess = loginContext.ExecuteLogin(MskTC.Text, TxtSifre.Text);

            if (isSuccess)
            {
                FrmSekreterDetay frs = new FrmSekreterDetay(notificationManager);
                frs.TCnumara = MskTC.Text;
                frs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Tc & Şifre");
            }
        }


        private void BtnGeri_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FrmGirisler)
                {
                    form.Show();
                    break;
                }
            }
            this.Hide();
        }

        private void FrmSekreterGiris_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Programdan çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void FrmSekreterGiris_Load(object sender, EventArgs e)
        {
            // Burada form yüklendiğinde yapılacak işlemleri ekleyebilirsiniz.
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Burada yapılacak işlemler (eğer varsa)
        }
    }
}
