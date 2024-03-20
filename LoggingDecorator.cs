using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace Proje_Hastane
{


    // LoggingDecorator.cs dosyası için güncellenmiş kod

    public class LoggingDecorator : FormDecorator
    {
        public LoggingDecorator(IFormComponent form) : base(form)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form), "Form nesnesi boş olamaz.");
            }
        }

        public override void Display()
        {
            if (decoratedForm != null)
            {
                LogToFile($"{decoratedForm.FormName} formu gösteriliyor.");
                base.Display();
            }
            else
            {
                throw new InvalidOperationException("Decorated form boş.");
            }
        }

        // 'new' anahtar kelimesi ile 'GetForm' metodunu tanımla
        public new Form GetForm()
        {
            return decoratedForm.GetForm();
        }

        private void LogToFile(string message)
        {
            try
            {
                // Log dosyasını uygulamanın çalıştırılabilir dosyasının bulunduğu dizinde oluştur
                string filePath = Path.Combine(Application.StartupPath, "log.txt");
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                // Hata oluştuğunda kullanıcıya bir hata mesajı göster
                MessageBox.Show($"Log kaydı yapılırken hata oluştu: {ex.Message}",
                                "Loglama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


}
