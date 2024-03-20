using System.Data.SqlClient;

namespace Proje_Hastane
{
    internal class sqlbaglantisi
    {
        private static sqlbaglantisi _instance;
        private static readonly object _lock = new object();

        private sqlbaglantisi()
        {
            // Özel kurucu (constructor)
        }

        public static sqlbaglantisi GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new sqlbaglantisi();
                    }
                }
            }
            return _instance;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection("Data Source=DESKTOP-1L6JQKC\\SQLEXPRESS;Initial Catalog=HastaneProjesi;Integrated Security=True");
        }
    }
}
