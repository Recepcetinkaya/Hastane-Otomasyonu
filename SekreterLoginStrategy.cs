using System;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public class SekreterLoginStrategy : ILoginStrategy
    {
        public bool Login(string username, string password)
        {
            using (SqlConnection conn = sqlbaglantisi.GetInstance().CreateConnection())
            {
                conn.Open();
                SqlCommand komut = new SqlCommand("Select * From Tbl_Sekreter where SekreterTC=@p1 and SekreterSifre=@p2", conn);
                komut.Parameters.AddWithValue("@p1", username);
                komut.Parameters.AddWithValue("@p2", password);

                using (SqlDataReader dr = komut.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        // Giriş başarılı, true döndür
                        return true;
                    }
                    else
                    {
                        // Giriş başarısız, false döndür
                        return false;
                    }
                }
            }
        }
    }
}
