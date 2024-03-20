using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NotificationManager notificationManager = new NotificationManager();

            IFormComponent girislerForm = new FrmGirisler(notificationManager);
            LoggingDecorator loggedGirislerForm = new LoggingDecorator(girislerForm);

            // Programın ana penceresi olarak FrmGirisler'i kullanın.
            Application.Run(loggedGirislerForm.GetForm());

            // Display metodunu Application.Run'dan sonra çağır
            loggedGirislerForm.Display();
        }

    }
}
