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
    public partial class FrmHastaGiris : Form, IFormComponent
    {
        private NotificationManager notificationManager;

        // IFormComponent arayüzünden gelen özellikler ve metodlar
        public string FormName
        {
            get { return "FrmHastaGiris"; }
        }

        public Form GetForm()
        {
            return this;
        }

        public void Display()
        {
            this.Show();
        }

        public FrmHastaGiris(NotificationManager notificationManager)
        {
            InitializeComponent();
            this.notificationManager = notificationManager;
        }
        private sqlbaglantisi bgl = sqlbaglantisi.GetInstance();

        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit fr = new FrmHastaKayit();
            fr.Show();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            var loginContext = new LoginContext(new HastaLoginStrategy());
            bool isSuccess = loginContext.ExecuteLogin(MskTC.Text, TxtSifre.Text);

            if (isSuccess)
            {
                FrmHastaDetay fr = new FrmHastaDetay(); // Eğer FrmHastaDetay da NotificationManager kullanıyorsa, burada da güncelleme yapılmalıdır.
                fr.tcno = MskTC.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC & Sifre");
            }
        }


        private void FrmHastaGiris_Load(object sender, EventArgs e)
        {
            // Form yüklenirken yapılacak işlemler buraya eklenebilir
        }

        private void FrmHastaGiris_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
