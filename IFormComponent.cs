using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proje_Hastane
{
    public interface IFormComponent
    {
        void Display();
        string FormName { get; }
        Form GetForm();  // Bu satırı ekleyin
    }

}
