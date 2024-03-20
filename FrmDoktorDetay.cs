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
using System.Xml.Linq;


namespace Proje_Hastane
{
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        public string DoktorTC;

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = DoktorTC;

            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();

                // Doktor Ad Soyad
                using (SqlCommand komut1 = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar Where DoktorTc=@p1", conn))
                {
                    komut1.Parameters.AddWithValue("@p1", DoktorTC);
                    using (SqlDataReader dr = komut1.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            LblAdSıyad.Text = dr[0] + " " + dr[1];
                        }
                    }
                } // SqlDataReader otomatik olarak kapatılır.

                // Randevular
                DataTable dt = new DataTable();
                using (SqlCommand komut2 = new SqlCommand("Select * From Tbl_Randevular where RandevuDoktor='" + LblAdSıyad.Text + "'", conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(komut2);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle fr = new FrmDoktorBilgiDuzenle();
            fr.DoktorBilgiTc = LblTC.Text;
            fr.Show();
        }

        private void BtnDuyurlar_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Geçerli bir satır ve sütun seçildiğinden emin olun
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (row.Cells.Count > 7) // 7 numaralı hücrenin varlığını kontrol et
                {
                    RcSikayet.Text = row.Cells[7].Value?.ToString(); // Güvenli bir şekilde değeri al
                }
            }
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

        private void FrmDoktorDetay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Programdan çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Environment.Exit(0);//Uygulama Tamamen Kapanır
            }
        }

        private void BtnCıkıs_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (Form form in Application.OpenForms)
                {
                    form.Hide(); // Tüm açık formların gizlenmesi
                }
                NotificationManager notificationManager = new NotificationManager();

                FrmGirisler frmAnaForm = new FrmGirisler(notificationManager); // Ana formunuzun yeni bir örneğinin oluşturulması
                frmAnaForm.Show(); // Yeni formun gösterilmesi
                Application.Exit(); // Uygulamanın tamamen kapatılması
            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where RandevuDoktor='" + LblAdSıyad.Text + "'", conn);
                da.Fill(dt);
            }
            dataGridView1.DataSource = dt;
        }

    }
}
