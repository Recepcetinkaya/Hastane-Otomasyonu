using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public class DoktorLoginStrategy : ILoginStrategy
    {
        public bool Login(string username, string password)
        {
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar Where Doktortc=@p1 and DoktorSifre=@p2", conn);
                komut.Parameters.AddWithValue("@p1", username);
                komut.Parameters.AddWithValue("@p2", password);
                SqlDataReader dr = komut.ExecuteReader();

                if (dr.Read())
                {
                    // Giriş başarılı
                    dr.Close();
                    return true;
                }
                else
                {
                    // Giriş başarısız
                    dr.Close();
                    return false;
                }
            }
        }
    }

}
