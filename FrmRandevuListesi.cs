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
    public partial class FrmRandevuListesi : Form
    {
        public FrmRandevuListesi()
        {
            InitializeComponent();
        }

        private sqlbaglantisi bgl = sqlbaglantisi.GetInstance();

        private void FrmRandevuListesi_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = bgl.CreateConnection())
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular", conn);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Burada DataGridView üzerindeki hücrelere tıklanınca yapılacak işlemleri ekleyebilirsiniz.
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Burada DataGridView üzerindeki hücrelere çift tıklanınca yapılacak işlemleri ekleyebilirsiniz.
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                using (SqlConnection conn = bgl.CreateConnection())
                {
                    int randevuId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Randevuid"].Value);
                    string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
                    string newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    SqlCommand cmd = new SqlCommand("UPDATE Tbl_Randevular SET " + columnName + "=@value WHERE Randevuid=@id", conn);
                    cmd.Parameters.AddWithValue("@value", newValue);
                    cmd.Parameters.AddWithValue("@id", randevuId);
                    cmd.ExecuteNonQuery();

                    LoadData();
                }
            }
        }
    }
}
