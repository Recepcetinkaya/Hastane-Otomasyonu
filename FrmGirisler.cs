using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class FrmGirisler : Form, IFormComponent
    {
        public string FormName
        {
            get { return "FrmGirisler"; }
        }
        public Form GetForm()
        {
            return this;
        }

        private NotificationManager notificationManager;

        public FrmGirisler(NotificationManager notificationManager)
        {
            InitializeComponent();
            this.notificationManager = notificationManager;
        }

        public void Display()
        {
            this.Show(); // Formu gösterir
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Kod buraya eklenebilir (eğer gerekliyse)
        }

        private void FrmGirisler_Load(object sender, EventArgs e)
        {
            // Kod buraya eklenebilir (eğer gerekliyse)
        }

        private void BtnDoktorGirisi_Click(object sender, EventArgs e)
        {
            // Doktor Giriş Formunu LoggingDecorator ile süsle ve göster
            IFormComponent doktorGirisForm = new FrmDoktorGiris(notificationManager) as IFormComponent;
            LoggingDecorator loggedDoktorGirisForm = new LoggingDecorator(doktorGirisForm);
            loggedDoktorGirisForm.Display();
            this.Hide();
        }

        private void BtnHastaGirisi_Click(object sender, EventArgs e)
        {
            IFormComponent hastaGirisForm = new FrmHastaGiris(notificationManager) as IFormComponent;
            if (hastaGirisForm == null)
            {
                MessageBox.Show("FrmHastaGiris formu IFormComponent olarak dönüştürülemedi.");
                return;
            }

            LoggingDecorator loggedHastaGirisForm = new LoggingDecorator(hastaGirisForm);
            loggedHastaGirisForm.Display();
            this.Hide();
        }





        private void BtnSekreterGirisi_Click(object sender, EventArgs e)
        {
            IFormComponent sekreterGirisForm = new FrmSekreterGiris(notificationManager) as IFormComponent;
            if (sekreterGirisForm == null)
            {
                MessageBox.Show("FrmSekreterGiris formu IFormComponent olarak dönüştürülemedi.");
                return;
            }

            LoggingDecorator loggedSekreterGirisForm = new LoggingDecorator(sekreterGirisForm);
            loggedSekreterGirisForm.Display();
            this.Hide();
        }


    }
}
