using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje_Hastane
{
    public class NotificationManager : INotificationSubject
    {
        private List<INotificationObserver> observers = new List<INotificationObserver>();
        private string message;

        public void RegisterObserver(INotificationObserver observer)
        {
            observers.Add(observer);
        }

        public void UnregisterObserver(INotificationObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
        }

        public void PostMessage(string message)
        {
            this.message = message;
            NotifyObservers();
        }
    }

}
