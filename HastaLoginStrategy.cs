using System;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public class HastaLoginStrategy : ILoginStrategy
    {
        public bool Login(string username, string password)
        {
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Select * From Tbl_Hastalar Where HastaTC=@p1 and HastaSifre=@p2", conn);
                komut.Parameters.AddWithValue("@p1", username);
                komut.Parameters.AddWithValue("@p2", password);

                using (SqlDataReader dr = komut.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        // Giriş başarılı
                        return true;
                    }
                    else
                    {
                        // Giriş başarısız
                        return false;
                    }
                }
            }
        }
    }
}
