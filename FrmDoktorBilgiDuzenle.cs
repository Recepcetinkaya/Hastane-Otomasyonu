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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = sqlbaglantisi.GetInstance();
        public string DoktorBilgiTc;

        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = DoktorBilgiTc;
            DoktorBilgileriGetir();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            // Olay işleyicisi kodu buraya yazılır
        }


        private void DoktorBilgileriGetir()
        {
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar where DoktorTC=@p1", conn);
                komut.Parameters.AddWithValue("@p1", MskTC.Text);
                using (SqlDataReader dr = komut.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        TxtAd.Text = dr[1].ToString();
                        TxtSoyad.Text = dr[2].ToString();
                        CmbBrans.Text = dr[3].ToString();
                        TxtSifre.Text = dr[5].ToString();
                    }
                }
            }
        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@p1, DoktorSoyad=@p2, DoktorBrans=@p3, DoktorSifre=@p4 where DoktorTc=@p5", conn);
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", CmbBrans.Text);
                komut.Parameters.AddWithValue("@p4", TxtSifre.Text);
                komut.Parameters.AddWithValue("@p5", MskTC.Text);
                komut.ExecuteNonQuery();
            }
            MessageBox.Show("Kayıt Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void FrmDoktorBilgiDuzenle_Load_1(object sender, EventArgs e)
        {

        }
    }
}
