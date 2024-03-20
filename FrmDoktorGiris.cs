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
    public partial class FrmDoktorGiris : Form, IFormComponent
    {
        private NotificationManager notificationManager;




        public void Display()
        {
            this.Show();
        }

        public Form GetForm()
        {
            return this;
        }

        public string FormName
        {
            get { return "FrmDoktorGiris"; }
        }

        // Yeni constructor: NotificationManager nesnesi alır
        public FrmDoktorGiris(NotificationManager notificationManager)
        {
            InitializeComponent();
            this.notificationManager = notificationManager;
        }

        // Eski constructor: Yeni constructor'ı çağırır
        public FrmDoktorGiris() : this(null) { }

        private void FrmDoktorGiris_Load(object sender, EventArgs e)
        {
            // Burada yapılacak işlemler (eğer varsa)
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            var loginContext = new LoginContext(new DoktorLoginStrategy());
            bool isSuccess = loginContext.ExecuteLogin(MskTC.Text, TxtSifre.Text);

            if (isSuccess)
            {
                FrmDoktorDetay fr = new FrmDoktorDetay(); // Eğer FrmDoktorDetay da NotificationManager kullanıyorsa, burada da güncelleme yapılmalıdır.
                fr.DoktorTC = MskTC.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı adı veya Şifre");
            }
        }


        private void FrmDoktorGiris_FormClosing(object sender, FormClosingEventArgs e)
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
