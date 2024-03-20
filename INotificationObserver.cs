using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje_Hastane
{
    public interface INotificationObserver
    {
        void Update(string message);
    }

}
