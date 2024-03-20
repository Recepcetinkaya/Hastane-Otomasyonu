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
    public partial class FrmDoktorPaneli : Form, INotificationObserver
    {
        public void Update(string message)
        {
            MessageBox.Show(message);
        }
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        // Form yüklenirken
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlDataAdapter da1 = new SqlDataAdapter("Select * From Tbl_Doktorlar", conn);
                da1.Fill(dt1);
            }
            dataGridView1.DataSource = dt1;

            // Branşları Combobox'a dahil etme
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", conn);
                SqlDataReader dr2 = komut2.ExecuteReader();
                while (dr2.Read())
                {
                    CmbBrans.Items.Add(dr2[0]);
                }
            }
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            string doktorAd = TxtAd.Text;
            string doktorSoyad = TxtSoyad.Text;
            string doktorTc = MskTC.Text;

            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand kontrolKomutu = new SqlCommand("SELECT COUNT(*) FROM Tbl_Doktorlar WHERE DoktorAd=@ad AND DoktorSoyad=@soyad AND DoktorTC=@tc", conn);
                kontrolKomutu.Parameters.AddWithValue("@ad", doktorAd);
                kontrolKomutu.Parameters.AddWithValue("@soyad", doktorSoyad);
                kontrolKomutu.Parameters.AddWithValue("@tc", doktorTc);

                int kayitSayisi = (int)kontrolKomutu.ExecuteScalar();

                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu doktor zaten kayıtlı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SqlCommand komut1 = new SqlCommand("insert into tbl_doktorlar (doktorad,doktorsoyad,doktorbrans,doktortc,doktorsifre) values (@d1,@d2,@d3,@d4,@d5)", conn);
                    komut1.Parameters.AddWithValue("@d1", doktorAd);
                    komut1.Parameters.AddWithValue("@d2", doktorSoyad);
                    komut1.Parameters.AddWithValue("@d3", CmbBrans.Text);
                    komut1.Parameters.AddWithValue("@d4", doktorTc);
                    komut1.Parameters.AddWithValue("@d5", TxtSifre.Text);
                    komut1.ExecuteNonQuery();

                    MessageBox.Show("Doktor Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            YenileDataGrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Delete from Tbl_Doktorlar where DoktorTC=@p1", conn);
                cmd.Parameters.AddWithValue("@p1", MskTC.Text);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Kayıt Silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            YenileDataGrid();
            Temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand guncelle = new SqlCommand("Update Tbl_Doktorlar set Doktorad=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,Doktorsifre=@d5 where doktortc=@d4", conn);
                guncelle.Parameters.AddWithValue("@d1", TxtAd.Text);
                guncelle.Parameters.AddWithValue("@d2", TxtSoyad.Text);
                guncelle.Parameters.AddWithValue("@d3", CmbBrans.Text);
                guncelle.Parameters.AddWithValue("@d4", MskTC.Text);
                guncelle.Parameters.AddWithValue("@d5", TxtSifre.Text);
                guncelle.ExecuteNonQuery();
            }

            MessageBox.Show("Doktor Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            YenileDataGrid();
            Temizle();
        }

        private void YenileDataGrid()
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlDataAdapter da1 = new SqlDataAdapter("Select * From Tbl_Doktorlar", conn);
                da1.Fill(dt1);
            }
            dataGridView1.DataSource = dt1;
        }

        private void Temizle()
        {
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            CmbBrans.Text = "";
            MskTC.Text = "";
            TxtSifre.Text = "";
        }
    }
}
