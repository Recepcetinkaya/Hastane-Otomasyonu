using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    namespace Proje_Hastane
    {
        public abstract class GirisEkraniFactory
        {
            protected NotificationManager notificationManager;

            public GirisEkraniFactory(NotificationManager notificationManager)
            {
                this.notificationManager = notificationManager;
            }

            public abstract Form GirisEkraniOlustur();
        }

        public class DoktorGirisEkraniFactory : GirisEkraniFactory
        {
            public DoktorGirisEkraniFactory(NotificationManager notificationManager)
                : base(notificationManager)
            {
            }

            public override Form GirisEkraniOlustur()
            {
                return new FrmDoktorGiris(notificationManager);
            }
        }

        public class HastaGirisEkraniFactory : GirisEkraniFactory
        {
            public HastaGirisEkraniFactory(NotificationManager notificationManager)
                : base(notificationManager)
            {
            }

            public override Form GirisEkraniOlustur()
            {
                return new FrmHastaGiris(notificationManager);
            }
        }

        public class SekreterGirisEkraniFactory : GirisEkraniFactory
        {
            public SekreterGirisEkraniFactory(NotificationManager notificationManager)
                : base(notificationManager)
            {
            }

            public override Form GirisEkraniOlustur()
            {
                return new FrmSekreterGiris(notificationManager);
            }
        }
    }
}
