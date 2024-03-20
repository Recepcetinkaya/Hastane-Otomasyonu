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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string tcno;
        private sqlbaglantisi bgl = sqlbaglantisi.GetInstance();

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tcno;
            HastaBilgisiGetir();
            RandevuGecmisiYukle();
            BranslariYukle();
        }

        private void HastaBilgisiGetir()
        {
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad From Tbl_Hastalar where HastaTc=@p1", conn);
                komut.Parameters.AddWithValue("@p1", LblTC.Text);
                using (SqlDataReader dr = komut.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        LblAdSıyad.Text = dr[0] + " " + dr[1];
                    }
                }
            }
        }

        private void RandevuGecmisiYukle()
        {
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where HastaTC=@tcno", conn);
                da.SelectCommand.Parameters.AddWithValue("@tcno", tcno);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }


        private void BranslariYukle()
        {
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", conn);
                using (SqlDataReader dr2 = komut2.ExecuteReader())
                {
                    while (dr2.Read())
                    {
                        CmbBrans.Items.Add(dr2[0]);
                    }
                }
            }
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand komut3 = new SqlCommand("Select DoktorAd, DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1", conn);
                komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
                using (SqlDataReader dr3 = komut3.ExecuteReader())
                {
                    while (dr3.Read())
                    {
                        CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
                    }
                }
            }
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where RandevuBrans='" + CmbBrans.Text + "' and RandevuDoktor='" + CmbDoktor.Text + "' and RandevuDurum=0", conn);
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCno = LblTC.Text;
            fr.Show();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Update Tbl_Randevular Set RandevuDurum=1, HastaTc=@p1, HastaSikayet=@p2 Where Randevuid=@p3", conn);
                komut.Parameters.AddWithValue("@p1", LblTC.Text);
                komut.Parameters.AddWithValue("@p2", RchSikayet.Text);
                komut.Parameters.AddWithValue("@p3", Txtid.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Randevu Alındı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void BtnGeri_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Öncelikle NotificationManager örneği oluşturun
            NotificationManager notificationManager = new NotificationManager();

            // Şimdi FrmGirisler'i bu parametre ile başlatın
            FrmGirisler frm = new FrmGirisler(notificationManager);
            frm.Show();

        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmHastaDetay_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Burada form kapatılmadan önce yapılacak işlemleri ekleyebilirsiniz.
            // Örneğin, kullanıcıya bir onay mesajı gösterilebilir:
            DialogResult result = MessageBox.Show("Programdan çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                e.Cancel = true; // Eğer "Hayır" seçilirse, formun kapanmasını iptal eder.
            }
        }

    }
}
