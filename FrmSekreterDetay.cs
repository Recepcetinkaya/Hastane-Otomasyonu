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
    public partial class FrmSekreterDetay : Form
    {
        private NotificationManager notificationManager;

        public FrmSekreterDetay(NotificationManager notificationManager)
        {
            InitializeComponent();
            this.notificationManager = notificationManager;
        }


        public string TCnumara;
        private sqlbaglantisi bgl = sqlbaglantisi.GetInstance();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = TCnumara;

            // Ad SoyAd Alma
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter Where SekreterTC=@p1", conn);
                komut1.Parameters.AddWithValue("@p1", LblTC.Text);
                using (SqlDataReader dr1 = komut1.ExecuteReader())
                {
                    while (dr1.Read())
                    {
                        LblAdSoyad.Text = dr1[0].ToString();
                    }
                }
            }

            // Branşları DataGride Aktarma
            DataTable dt1 = new DataTable();
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branslar", conn);
                da.Fill(dt1);
                dataGridView2.DataSource = dt1;
            }

            // Doktorlar Datagride Aktarma
            DataTable dt2 = new DataTable();
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd + ' ' +  DoktorSoyad) as 'Doktorlar',DoktorBrans From Tbl_Doktorlar", conn);
                da2.Fill(dt2);
                dataGridView3.DataSource = dt2;
            }

            // Branşları Comboboxa dahil etme
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", conn);
                SqlDataReader dr2 = komut2.ExecuteReader();
                while (dr2.Read())
                {
                    CmbBrans.Items.Add(dr2[0]);
                }
                dr2.Close();
            }

            // En yüksek randevu ID'sini al
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT MAX(Randevuid) FROM Tbl_Randevular", conn);
              
                int maxRandevuID = Convert.ToInt32(cmd.ExecuteScalar());
                Txtid.Text = (maxRandevuID + 1).ToString(); // En yüksek ID'ye 1 ekleyerek bir sonraki randevu ID'sini atama
            }
        }
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor,RandevuDurum,HastaTC) values (@r1,@r2,@r3,@r4,@r5,@r6)", conn);
                komutkaydet.Parameters.AddWithValue("@r1", MskTarih.Text);
                komutkaydet.Parameters.AddWithValue("@r2", MskSaat.Text);
                komutkaydet.Parameters.AddWithValue("@r3", CmbBrans.Text);
                komutkaydet.Parameters.AddWithValue("@r4", CmbDoktor.Text);
                komutkaydet.Parameters.AddWithValue("@r5", ChkDurum.Checked);
                komutkaydet.Parameters.AddWithValue("@r6", MskTC.Text);
                komutkaydet.ExecuteNonQuery();
                MessageBox.Show("Randevu Oluşturuldu.");
                Txtid.Text = (int.Parse(Txtid.Text) + 1).ToString(); // ID'yi bir sonraki değere artır
            }

            // Alanları sıfırlama
            MskTarih.Text = "";
            MskSaat.Text = "";
            CmbBrans.SelectedIndex = 0;
            CmbDoktor.SelectedIndex = 0;
            MskTC.Text = "";
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand komutbrans = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1", conn);
                komutbrans.Parameters.AddWithValue("@p1", CmbBrans.Text);
                SqlDataReader dr = komutbrans.ExecuteReader();
                while (dr.Read())
                {
                    CmbDoktor.Items.Add(dr[0] + " " + dr[1]);
                }
                dr.Close();
            }
        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = bgl.CreateConnection())
            {
                conn.Open();
                SqlCommand komut3 = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@d1)", conn);
                komut3.Parameters.AddWithValue("@d1", RchDuyuru.Text);
                komut3.ExecuteNonQuery();
                MessageBox.Show("Duyuru Oluşturuldu.");

                // Bildirim gönderme
                notificationManager.PostMessage("Yeni duyuru oluşturuldu: " + RchDuyuru.Text);
            }
        }


        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frm = new FrmBrans();
            frm.Show();
        }

        private void BtnListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frmrandevu = new FrmRandevuListesi();
            frmrandevu.Show();
        }

        private void BtnDuyurlar_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyurular = new FrmDuyurular();
            frmDuyurular.Show();
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
            if (MessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FrmSekreterDetay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Programdan çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            string bransad = dataGridView2.Rows[secilen].Cells["BransAd"].Value.ToString();
            CmbBrans.Text = bransad;
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView3.SelectedCells[0].RowIndex;
            string doktoradsoyad = dataGridView3.Rows[secilen].Cells[0].Value.ToString();
            CmbDoktor.Text = doktoradsoyad;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
