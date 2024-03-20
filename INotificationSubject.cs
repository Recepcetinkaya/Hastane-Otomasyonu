using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje_Hastane
{
    public interface INotificationSubject
    {
        void RegisterObserver(INotificationObserver observer);
        void UnregisterObserver(INotificationObserver observer);
        void NotifyObservers();
    }

}
