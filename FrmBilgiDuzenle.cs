﻿using System;
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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string TCno;

        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCno;
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Select * From Tbl_Hastalar where HastaTc=@p1 ", conn);
                komut.Parameters.AddWithValue("@p1", MskTC.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    TxtAd.Text = dr[1].ToString();
                    TxtSoyad.Text = dr[2].ToString();
                    MskTelefon.Text = dr[4].ToString();
                    TxtSifre.Text = dr[5].ToString();
                    CmbCinsiyet.Text = dr[6].ToString();
                }
            }
        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand komut2 = new SqlCommand("update Tbl_Hastalar set HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 where HastaTc=@p6", conn);
                komut2.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut2.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut2.Parameters.AddWithValue("@p3", MskTelefon.Text);
                komut2.Parameters.AddWithValue("@p4", TxtSifre.Text);
                komut2.Parameters.AddWithValue("@p5", CmbCinsiyet.Text);
                komut2.Parameters.AddWithValue("@p6", MskTC.Text);
                komut2.ExecuteNonQuery();
            }

            MessageBox.Show("Bilgileriniz Güncellendi. Ana menüye dönmek için OK'a tıklayın.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}