using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje_Hastane
{
    public interface ILoginStrategy
    {
        bool Login(string username, string password);
    }

}
