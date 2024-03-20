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
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        private sqlbaglantisi bgl = sqlbaglantisi.GetInstance();

        private void BtnKayitYap_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open(); // Bağlantıyı aç

                SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar (HastaAd, HastaSoyad, HastaTc, HastaTelefon, HastaSifre, HastaCinsiyet) values (@p1, @p2, @p3, @p4, @p5, @p6)", conn);

                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", MskTC.Text);
                komut.Parameters.AddWithValue("@p4", MskTelefon.Text);
                komut.Parameters.AddWithValue("@p5", TxtSifre.Text);
                komut.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);
                komut.ExecuteNonQuery();

                MessageBox.Show("Kaydınız Gerçekleşmiştir Şifreniz: " + TxtSifre.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Formun kapanması
            }
        }

        private void FrmHastaKayit_Load(object sender, EventArgs e)
        {
            // Form yüklenirken yapılacak işlemler buraya eklenebilir
        }
    }
}
