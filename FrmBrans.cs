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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }

        private void FrmBrans_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar", conn);
                da.Fill(dt);
            }
            dataGridView1.DataSource = dt;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand komutekle = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@b1)", conn);
                komutekle.Parameters.AddWithValue("@b1", TxtBrans.Text);
                komutekle.ExecuteNonQuery();
            }
            MessageBox.Show("Branş Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Txtid.Text = "";
            TxtBrans.Text = "";

            YenileDataGridView();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand komutsil = new SqlCommand("delete from Tbl_branslar where bransid=@b1", conn);
                komutsil.Parameters.AddWithValue("@b1", Txtid.Text);
                komutsil.ExecuteNonQuery();
            }
            MessageBox.Show("Branş Silindi");
            Txtid.Text = "";
            TxtBrans.Text = "";

            YenileDataGridView();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand komutguncelle = new SqlCommand("update Tbl_Branslar set BransAd=@p1 where Bransid=@p2", conn);
                komutguncelle.Parameters.AddWithValue("@p1", TxtBrans.Text);
                komutguncelle.Parameters.AddWithValue("@p2", Txtid.Text);
                komutguncelle.ExecuteNonQuery();
            }
            MessageBox.Show("Branş Güncellendi.");
            Txtid.Text = "";
            TxtBrans.Text = "";

            YenileDataGridView();
        }

        private void YenileDataGridView()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Branslar", conn);
                da.Fill(dt);
            }
            dataGridView1.DataSource = dt;
        }
    }
}
